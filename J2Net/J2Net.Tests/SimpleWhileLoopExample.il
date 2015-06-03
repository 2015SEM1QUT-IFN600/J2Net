.method public hidebysig static void  Main(string[] args) cil managed
{
  .entrypoint
  // Code size       20 (0x14)
  .maxstack  2
  .locals init (int32 V_0,
           bool V_1)
  IL_0000:  nop
  IL_0001:  ldc.i4.0
  IL_0002:  stloc.0
  IL_0003:  br.s       IL_000b
  IL_0005:  nop
  IL_0006:  ldloc.0
  IL_0007:  ldc.i4.1
  IL_0008:  add
  IL_0009:  stloc.0
  IL_000a:  nop
  IL_000b:  ldloc.0
  IL_000c:  ldc.i4.3
  IL_000d:  clt
  IL_000f:  stloc.1
  IL_0010:  ldloc.1
  IL_0011:  brtrue.s   IL_0005
  IL_0013:  ret
} // end of method WhileLoop::Main

