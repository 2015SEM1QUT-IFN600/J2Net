.class /*02000002*/ private auto ansi beforefieldinit DoWhile
       extends [mscorlib/*23000001*/]System.Object/*01000001*/
{
  .method /*06000001*/ private hidebysig static 
          void  Main(string[] args) cil managed
  {
    .entrypoint
    // Code size       8 (0x8)
    .maxstack  8
    IL_0000:  nop
    IL_0001:  call       void DoWhile/*02000002*/::doLoop() /* 06000002 */
    IL_0006:  nop
    IL_0007:  ret
  } // end of method DoWhile::Main

  .method /*06000002*/ public hidebysig static 
          void  doLoop() cil managed
  {
    // Code size       21 (0x15)
    .maxstack  2
    .locals /*11000001*/ init (int32 V_0,
             bool V_1)
    IL_0000:  nop
    IL_0001:  ldc.i4.1
    IL_0002:  stloc.0
    IL_0003:  nop
    IL_0004:  ldloc.0
    IL_0005:  ldc.i4.1
    IL_0006:  add
    IL_0007:  stloc.0
    IL_0008:  nop
    IL_0009:  ldloc.0
    IL_000a:  ldc.i4.3
    IL_000b:  cgt
    IL_000d:  ldc.i4.0
    IL_000e:  ceq
    IL_0010:  stloc.1
    IL_0011:  ldloc.1
    IL_0012:  brtrue.s   IL_0003

    IL_0014:  ret
  } // end of method DoWhile::doLoop

  .method /*06000003*/ public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Code size       7 (0x7)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [mscorlib/*23000001*/]System.Object/*01000001*/::.ctor() /* 0A000003 */
    IL_0006:  ret
  } // end of method DoWhile::.ctor

} // end of class DoWhile

