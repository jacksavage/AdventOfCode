[install][1] `Gleam.Gleam` via winget. this installs `Erlang.ErlangOTP` as a dependency. may need [rebar][2] if we use certain erlang dependencies.

```pwsh
winget install --id Gleam.Gleam
$env:PATH += ";$env:ProgramFiles\Erlang OTP\bin"
```

verify install by running project tests

```pwsh
cd .\gleam\src\
gleam test
```

tasks:

- [ ] consider using the javascript runtime over erlang

[1]: https://gleam.run/getting-started/installing/
[2]: https://rebar3.org/docs/getting-started/
