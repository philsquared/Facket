#r "Bin/Debug/Facket.exe"

open Context
open Types
open Manifest

let demoRoot = @"/Development/OSS/Facket/Demo"
let ctx = { 
    localDependenciesDir = demoRoot + "/local";
    localCacheDir = demoRoot + "/cache";
    remoteRepoDir = demoRoot + "/remote";
}

// Get timestamps
let localDepsTimestamp = Context.localDependenciesPath ctx |> Manifest.getManifestTimestamp
let localTimestamp = Context.localManifestPath ctx |> Manifest.getManifestTimestamp
let cacheTimestamp = Context.cacheManifestPath ctx |> Manifest.getManifestTimestamp
let remoteTimestamp = Context.remoteManifestPath ctx |> Manifest.getManifestTimestamp

let m = Context.remoteManifestPath ctx |> loadManifest

if localDepsTimestamp < localTimestamp || localDepsTimestamp < cacheTimestamp || localDepsTimestamp < remoteTimestamp then
    printf "[Out of date]\n"
else
    printf "[All up-to-date]\n"