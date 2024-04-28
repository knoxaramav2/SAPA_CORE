#compiler source
message(PROJECT_SOURCE_DIR="${NSemble_SOURCE_DIR}")
include_directories(${NSemble_SOURCE_DIR}/compiler/headers)
include_directories(${NSemble_SOURCE_DIR}/nlib/headers)

link_directories(${NSemble_SOURCE_DIR}/nlib/src)
add_executable(${NSemble_EXEC_NAME} main.cpp)
target_link_libraries(${NSemble_EXEC_NAME} nlib)