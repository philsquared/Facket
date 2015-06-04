module Utility

let formatUtcTime (dt:System.DateTime) = dt.ToString("s") + "Z"
let roughUtcTime (dt:System.DateTime) = ( dt.ToUniversalTime() |> formatUtcTime |> System.DateTime.Parse ).ToUniversalTime()
let readUtcTimestamp filename = filename |> System.IO.File.GetLastWriteTime |> roughUtcTime
