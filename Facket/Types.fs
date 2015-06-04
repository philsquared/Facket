module Types

type Manifest = { 
    packages : Map<string, System.DateTime>; 
    timestamp : System.DateTime 
}

type DependenciesManifest = { 
    packageMap : Map<string, string>; 
    timestamp : System.DateTime 
}

type Context = {
    localDependenciesDir : string;
    localCacheDir : string;
    remoteRepoDir : string
}