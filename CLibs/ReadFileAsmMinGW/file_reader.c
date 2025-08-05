//
// Created by Grey-Wind on 2025/8/5.
// Edited by Grey-Wind on 2025/8/5.
//

#include <windows.h>

// 防止C++名称修饰
#ifdef __cplusplus
extern "C" {
#endif

// 声明汇编函数
extern char* read_file_asm(const char* filename);

// 导出函数
__declspec(dllexport) const char* read_file(const char* filename) {
    char* result = read_file_asm(filename);
    return result ? result : "FILE_READ_ERROR";
}

__declspec(dllexport) void free_file_buffer(char* buffer) {
    free(buffer);
}

#ifdef __cplusplus
}
#endif
