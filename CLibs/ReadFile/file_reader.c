//
// Created by Grey-Wind on 2025/8/5.
// Edited by Grey-Wind on 2025/8/5.
//
#define WIN32_LEAN_AND_MEAN
#define _CRT_DECLARE_NONSTDC_NAMES 0
#define _WINSOCKAPI_
#define NOCRYPT
#define NOGDI
#define NOUSER
#define NOSERVICE
#define NOMCX
#define NOIME
#define NOSOUND
#define NOCOMM
#define NOHELP
#define NOPROFILER
#define NODEFERWINDOWPOS
#define NOMINMAX

#include "file_reader.h"
#include <windows.h>
#include <stdlib.h>

// 内部状态结构(对调用者透明)
typedef struct {
    HANDLE file_handle;
    HANDLE mapping_handle;
} InternalHandles;

int CALLCONV open_mapped_file(const wchar_t* file_path, MappedFileResult* result) {
    // 初始化结果结构
    result->mapped_view = NULL;
    result->file_size = 0;
    result->_internal = NULL;

    // 打开文件 (添加顺序扫描优化)
    HANDLE hFile = CreateFileW(
            file_path,
            GENERIC_READ,
            FILE_SHARE_READ,
            NULL,
            OPEN_EXISTING,
            FILE_ATTRIBUTE_NORMAL | FILE_FLAG_SEQUENTIAL_SCAN,
            NULL
    );

    if (hFile == INVALID_HANDLE_VALUE)
        return GetLastError();

    // 获取文件大小
    LARGE_INTEGER fileSize;
    if (!GetFileSizeEx(hFile, &fileSize)) {
        CloseHandle(hFile);
        return GetLastError();
    }

    // 处理空文件
    if (fileSize.QuadPart == 0) {
        CloseHandle(hFile);
        result->file_size = 0;
        return 0; // 空文件不是错误
    }

    // 创建内存映射
    HANDLE hMapping = CreateFileMappingW(
            hFile,
            NULL,
            PAGE_READONLY,
            0, 0,
            NULL
    );

    if (!hMapping) {
        DWORD err = GetLastError();
        CloseHandle(hFile);
        return err;
    }

    // 映射视图
    void* pView = MapViewOfFile(
            hMapping,
            FILE_MAP_READ,
            0, 0,
            (size_t)fileSize.QuadPart
    );

    if (!pView) {
        DWORD err = GetLastError();
        CloseHandle(hMapping);
        CloseHandle(hFile);
        return err;
    }

    // 设置返回结果
    result->mapped_view = pView;
    result->file_size = (size_t)fileSize.QuadPart;

    // 存储内部句柄
    InternalHandles* handles = malloc(sizeof(InternalHandles));
    if (!handles) {
        UnmapViewOfFile(pView);
        CloseHandle(hMapping);
        CloseHandle(hFile);
        return ERROR_NOT_ENOUGH_MEMORY;
    }

    handles->file_handle = hFile;
    handles->mapping_handle = hMapping;
    result->_internal = handles;

    return 0; // 成功
}

void CALLCONV free_mapped_file(MappedFileResult* result) {
    if (result->mapped_view) {
        UnmapViewOfFile(result->mapped_view);
        result->mapped_view = NULL;
    }

    if (result->_internal) {
        InternalHandles* handles = (InternalHandles*)result->_internal;

        if (handles->mapping_handle)
            CloseHandle(handles->mapping_handle);

        if (handles->file_handle)
            CloseHandle(handles->file_handle);

        free(handles);
        result->_internal = NULL;
    }

    result->file_size = 0;
}
