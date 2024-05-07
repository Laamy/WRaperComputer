using System;
using System.Collections.Generic;

/*

local CPU = {}
CPU.__index = CPU

type self = {
    Memory: {}; -- ram for our source to access for now
    Cache: {}; -- we'll store our bytecode in here (normally we combine ram(memory) and the source/cache but for now lets make it simple)
    PC: number; -- PC short for PROGRAM COUNTER it is the index we're on in the bytecode source
}

export type CPU = typeof(setmetatable({} :: self, CPU))

function CPU.new(): CPU
    local self = setmetatable({}:: self, CPU)

    -- temp
    self.Memory = {
        'H','e','l','l','o',' ','W','o','r','l','d','!', -- nil for now (we'll use the null terminated charcter later)
    }
    
    -- simple hello world program that repeats hello world over and over again!
    self.Cache = {
        0x10, -- PRINT instruction
        0x00, 0x01, -- pointer in memory (1)
        
        0x11, -- JMP instruction
        0x00, 0x01, -- pointer in memory (1)
    }

    self.PC = 1 -- first bytecode in our cached bytecode
    
    return self
end

function CPU:Tick()
    local byte = self.Cache[self.PC] -- lets get a simple PRINT instruction that takes in an index start in memory
    self.PC += 1
    
    --assert(byte > 0xFF, "[CPU] Bytecode contains bytes that are bigger then 8 bits")
    
    if byte == 0x10 then -- 0x0 will be our PRINT byte for now and it'll take in 2 bytes after that is the POINTER to where it is in memory
        -- read string from memory via its pointer
        local pointer = tonumber(self.Cache[self.PC]..self.Cache[self.PC+1]) -- Might slow down the CPU
        
        local str = ""
        while self.Memory[pointer] ~= nil do -- build string until NIL is detected
            str = str .. self.Memory[pointer]
            pointer += 1
        end
        
        -- skip the pointer
        self.PC += 2
        
        print(str)
    elseif byte == 0x11 then -- JMP instruction
        local pointer = tonumber(self.Cache[self.PC]..self.Cache[self.PC+1])
        
        self.PC = pointer -- JMP to the pointer
    end
    
    return byte
end

return CPU

*/
class CPU
{
    private List<char> Memory; // read write data memory
    private List<byte> Cache; // program cache
    private int PC; // program counter

    public CPU()
    {
        Memory = new List<char>()
        {
            'H','e','l','l','o',' ','W','o','r','l','d','!','\0'
        };

        Cache = new List<byte>()
        {
            0x10, // PRINT instruction
            0x00, 0x00, // pointer in memory (0)

            0x11, // JMP instruction
            0x00, 0x00 // pointer in memory (0)
        };

        PC = 0; // first bytecode in our cached bytecode
    }

    public void Tick()
    {
        byte instruction = Cache[PC];
        PC++;

        switch (instruction)
        {
            case (byte)InstructionByte.PRINT:
                {
                    int pointer = BitConverter.ToUInt16(new byte[] { Cache[PC], Cache[PC + 1] }, 0);
                    PC += 2;

                    string str = "";
                    while (Memory[pointer] != '\0')
                    {
                        str += Memory[pointer];
                        pointer++;
                    }

                    Console.WriteLine(str);
                }
                break;

            case (byte)InstructionByte.JMP:
                {
                    int pointer = BitConverter.ToUInt16(new byte[] { Cache[PC], Cache[PC + 1] }, 0);
                    PC = pointer;
                }
                break;
        }
    }
}