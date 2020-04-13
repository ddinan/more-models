CC=gcc
TARGET=MoreModels.so
RM=rm
SRC=MoreModels.c $(wildcard */*.c)
OBJ=$(patsubst %.c, %.o, $(SRC))

# Set this to the directory with ClassiCube header files.
INCLUDEDIR=$(HOME)/ClassiCube/src

CFLAGS=-I. -I$(INCLUDEDIR) -Wall -fpic
LFLAGS=-nostdlib -O3 -shared -s -z undefs

default: $(TARGET)

clean:
	$(RM) *.o */*.o
  
$(TARGET): $(OBJ)
	$(CC) $(LFLAGS) $(OBJ) -o $@
	
$(OBJ): %.o : %.c
	$(CC) $(CFLAGS) -c $< -o $@
