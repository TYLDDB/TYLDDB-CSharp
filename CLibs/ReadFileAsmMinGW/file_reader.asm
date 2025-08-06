; 适配32位和64位调用约定
section .text

; 导出函数
global read_file_asm

; 导入Windows API函数
extern __imp_CreateFileA
extern __imp_GetFileSizeEx
extern __imp_ReadFile
extern __imp_CloseHandle
extern malloc
extern free

; 常量定义
GENERIC_READ            equ 0x80000000
FILE_SHARE_READ         equ 0x00000001
OPEN_EXISTING           equ 3
FILE_ATTRIBUTE_NORMAL   equ 0x80
INVALID_HANDLE_VALUE    equ -1

; char* read_file_asm(const char* filename)
read_file_asm:
%ifidn __OUTPUT_FORMAT__, win64
    ; === 64位实现 ===
    push rbx
    push rsi
    push rdi
    push rbp
    mov rbp, rsp

    ; 参数: rcx = filename
    mov rsi, rcx        ; 保存文件名指针

    ; 调用CreateFileA (使用RIP相对寻址)
    xor r9, r9          ; lpSecurityAttributes = NULL
    xor r8, r8          ; dwCreationDisposition = OPEN_EXISTING
    mov rdx, GENERIC_READ
    mov rcx, rsi        ; lpFileName
    mov dword [rsp+32], FILE_ATTRIBUTE_NORMAL
    mov qword [rsp+40], 0 ; hTemplateFile = NULL
    mov rax, [rel __imp_CreateFileA] ; 使用RIP相对寻址
    call rax

    cmp rax, INVALID_HANDLE_VALUE
    je .error
    mov rbx, rax        ; 保存文件句柄

    ; 调用GetFileSizeEx
    sub rsp, 16
    lea rdx, [rsp+8]    ; lpFileSize
    mov rcx, rbx        ; hFile
    mov rax, [rel __imp_GetFileSizeEx] ; RIP相对寻址
    call rax
    test rax, rax
    jz .close_error

    ; 分配内存 (文件大小 + 1)
    mov rcx, [rsp+8]    ; 文件大小
    inc rcx             ; +1 for null
    call malloc
    test rax, rax
    jz .close_error
    mov rdi, rax        ; 保存缓冲区指针

    ; 调用ReadFile
    mov rcx, rbx        ; hFile
    mov rdx, rdi        ; lpBuffer
    mov r8, [rsp+8]     ; nNumberOfBytesToRead
    lea r9, [rsp]       ; lpNumberOfBytesRead
    mov qword [rsp+32], 0 ; lpOverlapped = NULL
    mov rax, [rel __imp_ReadFile] ; RIP相对寻址
    call rax
    test rax, rax
    jz .free_error

    ; 添加null终止符
    mov rcx, [rsp+8]    ; 文件大小
    mov byte [rdi + rcx], 0

    ; 关闭文件句柄
    mov rcx, rbx
    mov rax, [rel __imp_CloseHandle] ; RIP相对寻址
    call rax

    ; 清理栈并返回
    add rsp, 16
    mov rax, rdi
    jmp .exit

%else
    ; === 32位实现 ===
    push ebx
    push esi
    push edi
    push ebp
    mov ebp, esp

    ; 参数: [ebp+20] = filename
    mov esi, [ebp+20]   ; 保存文件名指针

    ; 调用CreateFileA
    push 0              ; hTemplateFile = NULL
    push FILE_ATTRIBUTE_NORMAL
    push OPEN_EXISTING
    push 0              ; lpSecurityAttributes = NULL
    push FILE_SHARE_READ
    push GENERIC_READ
    push esi            ; lpFileName
    call [__imp_CreateFileA]
    add esp, 28

    cmp eax, INVALID_HANDLE_VALUE
    je .error
    mov ebx, eax        ; 保存文件句柄

    ; 调用GetFileSizeEx
    sub esp, 8          ; 分配LARGE_INTEGER空间
    lea eax, [esp]      ; lpFileSize
    push eax
    push ebx            ; hFile
    call [__imp_GetFileSizeEx]
    add esp, 8
    test eax, eax
    jz .close_error

    ; 分配内存 (文件大小 + 1)
    mov ecx, [esp]      ; 文件大小低32位
    inc ecx             ; +1 for null
    push ecx
    call malloc
    add esp, 4
    test eax, eax
    jz .close_error
    mov edi, eax        ; 保存缓冲区指针

    ; 调用ReadFile
    lea eax, [esp+4]    ; lpNumberOfBytesRead
    push 0              ; lpOverlapped = NULL
    push eax
    push [esp+8]        ; nNumberOfBytesToRead
    push edi            ; lpBuffer
    push ebx            ; hFile
    call [__imp_ReadFile]
    add esp, 20
    test eax, eax
    jz .free_error

    ; 添加null终止符
    mov ecx, [esp]      ; 文件大小
    mov byte [edi + ecx], 0

    ; 关闭文件句柄
    push ebx
    call [__imp_CloseHandle]
    add esp, 4

    ; 清理栈并返回
    add esp, 8
    mov eax, edi
    jmp .exit
%endif

.free_error:
%ifidn __OUTPUT_FORMAT__, win64
    mov rcx, rdi
%else
    push edi
%endif
    call free
%ifidn __OUTPUT_FORMAT__, win32
    add esp, 4
%endif

.close_error:
%ifidn __OUTPUT_FORMAT__, win64
    mov rcx, rbx
    mov rax, [rel __imp_CloseHandle]
    call rax
%else
    push ebx
    call [__imp_CloseHandle]
    add esp, 4
%endif

.error:
%ifidn __OUTPUT_FORMAT__, win64
    xor rax, rax
%else
    xor eax, eax
%endif

.exit:
%ifidn __OUTPUT_FORMAT__, win64
    pop rbp
    pop rdi
    pop rsi
    pop rbx
%else
    pop ebp
    pop edi
    pop esi
    pop ebx
%endif
    ret