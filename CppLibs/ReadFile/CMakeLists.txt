cmake_minimum_required(VERSION 3.31)
project(ReadFile)

set(CMAKE_CXX_STANDARD 17)

add_library(ReadFileCpp SHARED TextReader.cpp
        TextReader.h)
