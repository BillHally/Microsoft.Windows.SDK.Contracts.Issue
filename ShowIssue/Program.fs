module FSharp.Main

open System

type ThisFailsAtRuntime =
    static member A() =
        async {
            let locator = Windows.Devices.Geolocation.Geolocator()
            let! location = Async.AwaitTask (locator.GetGeopositionAsync().AsTask())
            let position = location.Coordinate.Point.Position
            Console.WriteLine("lat:{0}, long:{1}", position.Latitude, position.Longitude)
        }
        |> Async.RunSynchronously

[<EntryPoint>]
let main args =
    ThisFailsAtRuntime.A()
    //async {
    //    let! __ = Async.AwaitTask (CSharp.Program.Main args)
    //    ()
    //}
    //|> Async.RunSynchronously
    0
