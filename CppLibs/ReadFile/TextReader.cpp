//
// Created by Grey-Wind on 2025/8/6.
//

// 在包含头文件前定义导出宏
#define TEXTREADER_EXPORTS

#include "TextReader.h"
#include <cstdint>

#ifdef _WIN32
#include <Windows.h>
#else
#include <fcntl.h>
#include <sys/mman.h>
#include <sys/stat.h>
#include <unistd.h>
#include <cstring>
#include <cstdlib>
#endif

// 跨平台文件句柄结构
struct FileHandle {
#ifdef _WIN32
    HANDLE hFile = INVALID_HANDLE_VALUE;
    HANDLE hMapping = NULL;
#else
    int fd = -1;
#endif
    void* pData = nullptr;
    long long fileSize = 0;
};

TEXTREADER_API FileHandle* OpenTextFile(const wchar_t* filePath) {
    FileHandle* handle = new FileHandle();
    if (!handle) return nullptr;

#ifdef _WIN32
    // Windows 实现
    handle->hFile = CreateFileW(
            filePath,
            GENERIC_READ,
            FILE_SHARE_READ,
            NULL,
            OPEN_EXISTING,
            FILE_ATTRIBUTE_NORMAL | FILE_FLAG_SEQUENTIAL_SCAN,
            NULL
    );

    if (handle->hFile == INVALID_HANDLE_VALUE) {
        delete handle;
        return nullptr;
    }

    LARGE_INTEGER size;
    if (!GetFileSizeEx(handle->hFile, &size)) {
        CloseHandle(handle->hFile);
        delete handle;
        return nullptr;
    }
    handle->fileSize = size.QuadPart;

    if (handle->fileSize == 0) {
        // 空文件特殊处理
        CloseHandle(handle->hFile);
        handle->pData = nullptr;
        return handle;
    }

    handle->hMapping = CreateFileMappingW(
            handle->hFile,
            NULL,
            PAGE_READONLY,
            0, 0,
            NULL
    );

    if (!handle->hMapping) {
        CloseHandle(handle->hFile);
        delete handle;
        return nullptr;
    }

    handle->pData = MapViewOfFile(
            handle->hMapping,
            FILE_MAP_READ,
            0, 0,
            (size_t)handle->fileSize
    );

    if (!handle->pData) {
        CloseHandle(handle->hMapping);
        CloseHandle(handle->hFile);
        delete handle;
        return nullptr;
    }
#else
    // Linux/MacOS 实现 (MinGW 兼容)
    char* utf8Path = (char*)malloc(wcslen(filePath) * 4 + 1);
    size_t count = wcstombs(utf8Path, filePath, wcslen(filePath) * 4 + 1);

    if (count == (size_t)-1) {
        free(utf8Path);
        delete handle;
        return nullptr;
    }

    handle->fd = open(utf8Path, O_RDONLY);
    free(utf8Path);

    if (handle->fd == -1) {
        delete handle;
        return nullptr;
    }

    struct stat sb;
    if (fstat(handle->fd, &sb) == -1) {
        close(handle->fd);
        delete handle;
        return nullptr;
    }
    handle->fileSize = sb.st_size;

    if (handle->fileSize == 0) {
        // 空文件特殊处理
        close(handle->fd);
        handle->pData = nullptr;
        return handle;
    }

    handle->pData = mmap(
        nullptr,
        handle->fileSize,
        PROT_READ,
        MAP_PRIVATE,
        handle->fd,
        0
    );

    if (handle->pData == MAP_FAILED) {
        close(handle->fd);
        delete handle;
        return nullptr;
    }

    // 建议顺序读取模式
    #if defined(__linux__) || defined(__APPLE__)
    madvise(handle->pData, handle->fileSize, MADV_SEQUENTIAL);
    #endif
#endif

    return handle;
}

TEXTREADER_API long long GetFileSize(FileHandle* handle) {
    return handle ? handle->fileSize : 0;
}

TEXTREADER_API const char* GetFileContent(FileHandle* handle) {
    return handle ? static_cast<const char*>(handle->pData) : nullptr;
}

TEXTREADER_API void CloseTextFile(FileHandle* handle) {
    if (!handle) return;

    if (handle->pData && handle->fileSize > 0) {
#ifdef _WIN32
        UnmapViewOfFile(handle->pData);
#else
        munmap(handle->pData, handle->fileSize);
#endif
    }

#ifdef _WIN32
    if (handle->hMapping) CloseHandle(handle->hMapping);
    if (handle->hFile != INVALID_HANDLE_VALUE) CloseHandle(handle->hFile);
#else
    if (handle->fd != -1) close(handle->fd);
#endif

    delete handle;
}
