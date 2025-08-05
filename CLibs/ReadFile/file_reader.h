//
// Created by Grey-Wind on 2025/8/5.
// Edited by Grey-Wind on 2025/8/5.
//

#ifndef READFILE_FILE_READER_H
#define READFILE_FILE_READER_H

#include <stddef.h>

#ifdef __cplusplus
extern "C" {
#endif

typedef struct {
    void* mapped_view;   // 内存映射视图指针
    size_t file_size;    // 文件大小(字节)
    void* _internal;     // 内部句柄(不透明结构)
} MappedFileResult;

// 打开文件并创建内存映射
// 成功返回0，失败返回错误代码(非0)
int open_mapped_file(const wchar_t* file_path, MappedFileResult* result);

// 释放文件映射资源
void free_mapped_file(MappedFileResult* result);

#ifdef __cplusplus
}
#endif

#endif //READFILE_FILE_READER_H
