  IL_0001:  ldc.i4.0
  IL_0002:  stloc.0
  IL_0003:  br.s       IL_0009
  IL_0005:  ldloc.0
  IL_0006:  ldc.i4.1
  IL_0007:  add
  IL_0008:  stloc.0
  IL_0009:  ldloc.0
  IL_000a:  ldc.i4.5
  IL_000b:  clt
  IL_000d:  stloc.1
  IL_000e:  ldloc.1
  IL_000f:  brtrue.s   IL_0005
  IL_0011:  ret
