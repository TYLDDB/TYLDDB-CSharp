cmake_minimum_required(VERSION 3.31)
project(ReadFileAsmMinGW C)

# 设置NASM汇编器
enable_language(ASM_NASM)

# 根据架构设置
if(CMAKE_SIZEOF_VOID_P EQUAL 8)
    set(CMAKE_ASM_NASM_OBJECT_FORMAT win64)
    set(ASM_DEFINITIONS "-D__OUTPUT_FORMAT__=win64")
else()
    set(CMAKE_ASM_NASM_OBJECT_FORMAT win32)
    set(ASM_DEFINITIONS "-D__OUTPUT_FORMAT__=win32")
endif()

set(CMAKE_C_STANDARD 11)

add_library(RFAsmM SHARED file_reader.asm file_reader.c)

# 设置汇编标志
set_source_files_properties(file_reader.asm PROPERTIES
        COMPILE_FLAGS "${ASM_DEFINITIONS}")

# 链接系统库
target_link_libraries(RFAsmM kernel32)
