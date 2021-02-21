# ArgSplitter

ArgSplitter is a small .NET Standard 2.0 compatible library to split a string into command-line args considering escape sequences and quotes

## Usage

ArgSplitter extends the string type and adds the method `SplitArgs`.

```csharp
//Input string: arg1 arg2 "arg3 arg3 arg3" arg4\ arg4 arg5
string myArgString = "arg1 arg2 \"arg3 arg3 arg3\" arg4\\ arg4 arg5";

//Call using extension method
string[] args = myArgString.SplitArgs();

//Call using ArgSplitter-class
string[] args = ArgSplitter.SplitArgs(myArgString);


//args: ["arg1", "arg2", "arg3 arg3 arg3", "arg4 arg4", "arg5"]
```

## `SplitArgs` method

#### Args

- `input`: string
- `maxParts`: int, default `int.MaxValue`  
  String will be split into a maximum parts of `maxParts`
- `removeAllEscapeSequences`: bool, default `false`  
  By default, only space, `"` and `\` are escaped. All other backslashes are kept. If `true` is passed, all (not escaped) backslashes will be removed.  
  **Note**: Known escape sequences like `\t`, `\r` or `\n` aren't resolved! The backslashes are just removed.

#### Returns

`IEnumerable<string>` containing the arguments

## License

ArgSplitter is licensed under the MIT License
