//
// Created by Grey-Wind on 2025/8/5.
// Edited by Grey-Wind on 2025/8/5.
//

#ifndef READFILE_FILE_READER_H
#define READFILE_FILE_READER_H

#include <stddef.h>

#ifdef _WIN32
#define CALLCONV __stdcall
#define DLLEXPORT __declspec(dllexport)
#else
#define CALLCONV
    #define DLLEXPORT
#endif

#ifdef __cplusplus
extern "C" {
#endif

typedef struct {
    void* mapped_view;
    size_t file_size;
    void* _internal;
} MappedFileResult;

DLLEXPORT int CALLCONV open_mapped_file(const wchar_t* file_path, MappedFileResult* result);
DLLEXPORT void CALLCONV free_mapped_file(MappedFileResult* result);

#ifdef __cplusplus
}
#endif

#endif // READFILE_FILE_READER_H
