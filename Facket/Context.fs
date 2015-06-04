module Context

open System.IO
open Types

let dependenciesFilename = "facket.dependencies"
let manifestFilename = "facket.manifest"

let localDependenciesPath (ctx:Context) = Path.Combine( ctx.localDependenciesDir, dependenciesFilename )
let localManifestPath (ctx:Context) = Path.Combine( ctx.localDependenciesDir, manifestFilename )
let cacheManifestPath (ctx:Context) = Path.Combine( ctx.localCacheDir, manifestFilename )
let remoteManifestPath (ctx:Context) = Path.Combine( ctx.remoteRepoDir, manifestFilename )
