open System

open Mono.Cecil
open System.IO

[<EntryPoint>]
let main argv =
    let assembly = argv.[0]
    let windowsRuntimeAssemblies = argv.[1..]

    use memoryStream = new MemoryStream()

    do
        printfn "Loading %s..." assembly
        use modu = ModuleDefinition.ReadModule(assembly)
        for reference in modu.AssemblyReferences do
            if windowsRuntimeAssemblies |> Array.contains reference.Name then
                printfn "Marking reference to %s as WindowsRuntime..." reference.Name
                reference.IsWindowsRuntime <- true

        modu.Write(memoryStream)

    memoryStream.Seek(0L, SeekOrigin.Begin) |> ignore<int64>
    printfn "Writing %s..." assembly
    File.WriteAllBytes(assembly, memoryStream.GetBuffer())

    0
