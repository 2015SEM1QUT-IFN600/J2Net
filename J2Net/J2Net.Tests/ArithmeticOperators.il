.method private hidebysig static void  Main(string[] args) cil managed
{
  .entrypoint
  // Code size       164 (0xa4)
  .maxstack  2
  .locals init (int32 V_0,
           int32 V_1,
           int32 V_2,
           int32 V_3,
           int32 V_4,
           float32 V_5)
  IL_0000:  nop
  IL_0001:  ldstr      "Enter first number\t\t"
  IL_0006:  call       void [mscorlib]System.Console::Write(string)
  IL_000b:  nop
  IL_000c:  call       string [mscorlib]System.Console::ReadLine()
  IL_0011:  call       int32 [mscorlib]System.Convert::ToInt32(string)
  IL_0016:  stloc.0
  IL_0017:  ldstr      "\n\nEnter second number\t\t"
  IL_001c:  call       void [mscorlib]System.Console::Write(string)
  IL_0021:  nop
  IL_0022:  call       string [mscorlib]System.Console::ReadLine()
  IL_0027:  call       int32 [mscorlib]System.Convert::ToInt32(string)
  IL_002c:  stloc.1
  IL_002d:  ldloc.0
  IL_002e:  ldloc.1
  IL_002f:  add
  IL_0030:  stloc.2
  IL_0031:  ldloc.0
  IL_0032:  ldloc.1
  IL_0033:  sub
  IL_0034:  stloc.3
  IL_0035:  ldloc.0
  IL_0036:  ldloc.1
  IL_0037:  mul
  IL_0038:  stloc.s    V_4
  IL_003a:  ldloc.0
  IL_003b:  conv.r4
  IL_003c:  ldloc.1
  IL_003d:  conv.r4
  IL_003e:  div
  IL_003f:  stloc.s    V_5
  IL_0041:  ldstr      "\n\n=====================\n"
  IL_0046:  call       void [mscorlib]System.Console::WriteLine(string)
  IL_004b:  nop
  IL_004c:  ldstr      "Addition\t\t{0}"
  IL_0051:  ldloc.2
  IL_0052:  box        [mscorlib]System.Int32
  IL_0057:  call       void [mscorlib]System.Console::WriteLine(string,
                                                                object)
  IL_005c:  nop
  IL_005d:  ldstr      "Subtraction\t\t{0}"
  IL_0062:  ldloc.3
  IL_0063:  box        [mscorlib]System.Int32
  IL_0068:  call       void [mscorlib]System.Console::WriteLine(string,
                                                                object)
  IL_006d:  nop
  IL_006e:  ldstr      "Multiplication\t\t{0}"
  IL_0073:  ldloc.s    V_4
  IL_0075:  box        [mscorlib]System.Int32
  IL_007a:  call       void [mscorlib]System.Console::WriteLine(string,
                                                                object)
  IL_007f:  nop
  IL_0080:  ldstr      "Division\t\t{0}"
  IL_0085:  ldloc.s    V_5
  IL_0087:  box        [mscorlib]System.Single
  IL_008c:  call       void [mscorlib]System.Console::WriteLine(string,
                                                                object)
  IL_0091:  nop
  IL_0092:  ldstr      "\n=======================\n"
  IL_0097:  call       void [mscorlib]System.Console::WriteLine(string)
  IL_009c:  nop
  IL_009d:  call       string [mscorlib]System.Console::ReadLine()
  IL_00a2:  pop
  IL_00a3:  ret
} // end of method ArithmeticOperators::Main

