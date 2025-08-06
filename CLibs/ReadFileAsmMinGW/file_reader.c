//
// Created by Grey-Wind on 2025/8/5.
// Edited by Grey-Wind on 2025/8/6.
//

#include <windows.h>
#include <stdio.h>

// 防止C++名称修饰
#ifdef __cplusplus
extern "C" {
#endif

// 添加调试输出
void DebugOutput(const char* message) {
    OutputDebugStringA(message);
    FILE* log = fopen("native_debug.log", "a");
    if (log) {
        fprintf(log, "%s\n", message);
        fclose(log);
    }
}

// 声明汇编函数
extern char* read_file_asm(const char* filename);

// 导出函数
__declspec(dllexport) const char* read_file(const char* filename) {
    DebugOutput("进入 read_file 函数");
    DebugOutput(filename);

    char* result = read_file_asm(filename);

    if (result) {
        DebugOutput("文件读取成功");
        // 记录前16字节内容
        char buffer[50];
        snprintf(buffer, sizeof(buffer), "内容前16字节: %.16s", result);
        DebugOutput(buffer);
    } else {
        DebugOutput("文件读取失败");
    }

    return result ? result : "FILE_READ_ERROR";
}

__declspec(dllexport) void free_file_buffer(char* buffer) {
    free(buffer);
}

#ifdef __cplusplus
}
#endif
