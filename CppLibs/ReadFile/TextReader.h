//
// Created by Grey-Wind on 2025/8/6.
//

#pragma once

#ifndef READFILE_TEXTREADER_H
#define READFILE_TEXTREADER_H

// 跨平台导出宏定义
#if defined(_WIN32)
#if defined(TextReader_EXPORTS) || defined(TEXTREADER_EXPORTS)
#define TEXTREADER_API __declspec(dllexport)
#else
#define TEXTREADER_API __declspec(dllimport)
#endif
#else
#if defined(TextReader_EXPORTS) || defined(TEXTREADER_EXPORTS)
        #define TEXTREADER_API __attribute__((visibility("default")))
    #else
        #define TEXTREADER_API
    #endif
#endif

#ifdef __cplusplus
extern "C" {
#endif

// 文件句柄结构（对C#透明）
struct FileHandle;

// 打开文件并映射内存
TEXTREADER_API FileHandle* OpenTextFile(const wchar_t* filePath);

// 获取文件大小（重命名避免Windows API冲突）
TEXTREADER_API long long GetTextFileSize(FileHandle* handle);

// 获取内存映射起始地址
TEXTREADER_API const char* GetFileContent(FileHandle* handle);

// 关闭文件并释放资源
TEXTREADER_API void CloseTextFile(FileHandle* handle);

#ifdef __cplusplus
}
#endif

#endif //READFILE_TEXTREADER_H
