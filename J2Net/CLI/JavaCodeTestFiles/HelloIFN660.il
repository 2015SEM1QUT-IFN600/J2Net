.assembly extern mscorlib
{
}
.assembly ildasmTest
{
}

.class public ildasmTest.Program
{
  .field private int32 k
  .method public static void Main(string[] args)
  {
    .entrypoint
    .maxstack  2
    .locals init ([0] int32 i,
             [1] int32 k)
    ldc.i4.s   42
    stloc.0
    ldloc.0
    ldc.i4.1
    add
    stloc.1
    call       class [mscorlib]System.IO.TextWriter [mscorlib]System.Console::get_Out()
    ldloc.1
    callvirt   instance void [mscorlib]System.IO.TextWriter::WriteLine(int32)
    ret
  }
}