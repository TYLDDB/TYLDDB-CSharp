cmake_minimum_required(VERSION 3.31)
project(ReadFile C)

set(CMAKE_C_STANDARD 11)

add_library(ReadFile SHARED
        file_reader.c
        file_reader.h)
