module Manifest

open System.IO
open Utility
open Types

let notBlankLineOrComment (line:string) = line.Trim() <> "" && not (line.Trim().StartsWith "#")

let maxTimestamp (manifest:Manifest) = 
    manifest.packages |> Seq.map( fun kvp -> kvp.Value ) |> Seq.max

let loadDependenciesManifest filename = 
    let packageMap = File.ReadLines filename
                        |> Seq.filter notBlankLineOrComment
                        |> Seq.map( fun (line:string) -> 
                                match line.Split( [|"=>"|], System.StringSplitOptions.None ) with
                                | [|package|]           -> (package.Trim(), Path.GetDirectoryName (package.Trim()))
                                | [|package; target|]   -> (package.Trim(), target.Trim())
                                | _                     -> failwithf "Could not parse '%s'" line
                            )
                        |> Map.ofSeq
    { packageMap = packageMap; timestamp = readUtcTimestamp filename }

let loadManifest filename =
    let packages = File.ReadLines filename
                    |> Seq.filter notBlankLineOrComment
                    |> Seq.map( fun (line:string) -> 
                            match line.Trim().Split( [|" "|], System.StringSplitOptions.None ) with
                            | [|timestamp; package|] -> (package, (System.DateTime.Parse timestamp).ToUniversalTime())
                            | _ -> failwithf "Could not parse '%s'" line
                        )
                    |> Map.ofSeq
    { packages = packages; timestamp = readUtcTimestamp filename }

let createManifest dir =
    let packages = Directory.EnumerateDirectories dir 
                    |> Seq.collect( fun p -> Directory.GetFiles( p, "*.zip" ) 
                                            |> Seq.map( fun z -> (z.Substring( dir.Length+1 ), readUtcTimestamp z ) ) )
                    |> Map.ofSeq
    let maxTimestamp = packages |> Seq.map( fun kvp -> kvp.Value ) |> Seq.max
    { packages = packages; timestamp = maxTimestamp }

let writeManifest (manifest:Manifest) filename =
    let lines = manifest.packages |> Seq.map( fun kvp -> sprintf "%s %s" (kvp.Value |> formatUtcTime) (kvp.Key) )
    File.WriteAllLines( filename, lines )
    ()

let getManifestTimestamp filename =
    if File.Exists filename then
        readUtcTimestamp filename
    else
        System.DateTime.MinValue


//loadDependenciesManifest "/Development/OSS/Facket/Demo/local/facket.dependencies"
//let m = createManifest "/Development/OSS/Facket/Demo/remote"
//writeManifest m "/Development/OSS/Facket/Demo/remote/facket.manifest"
//let m = loadManifest "/Development/OSS/Facket/Demo/remote/facket.manifest"
