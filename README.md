[![Build status](https://ci.appveyor.com/api/projects/status/xcfyex0tjp3c6m18?svg=true)](https://ci.appveyor.com/project/lucasteles/modelviewbinder)
[![License](http://img.shields.io/:license-mit-blue.svg)](http://csmacnz.mit-license.org)
[![Nuget](https://img.shields.io/nuget/v/Automateles.svg)](https://www.nuget.org/packages/Automateles/)

# Automateles
Simple implementation of pushdown automaton
Reads strings as input and change states based on enums

## Installing
To install from Nuget

```powershell
Install-Package Automateles 
```

* Defines a basic automaton

[Accept only the string "abcd"](http://automatonsimulator.com/#%7B%22type%22%3A%22DFA%22%2C%22dfa%22%3A%7B%22transitions%22%3A%7B%22start%22%3A%7B%22a%22%3A%22s0%22%7D%2C%22s0%22%3A%7B%22b%22%3A%22s1%22%7D%2C%22s1%22%3A%7B%22c%22%3A%22s2%22%7D%2C%22s2%22%3A%7B%22d%22%3A%22s3%22%7D%7D%2C%22startState%22%3A%22start%22%2C%22acceptStates%22%3A%5B%22s3%22%5D%7D%2C%22states%22%3A%7B%22start%22%3A%7B%7D%2C%22s0%22%3A%7B%22top%22%3A252%2C%22left%22%3A176%7D%2C%22s1%22%3A%7B%22top%22%3A251%2C%22left%22%3A367%7D%2C%22s2%22%3A%7B%22top%22%3A253%2C%22left%22%3A574%7D%2C%22s3%22%3A%7B%22isAccept%22%3Atrue%2C%22top%22%3A256%2C%22left%22%3A738%7D%7D%2C%22transitions%22%3A%5B%7B%22stateA%22%3A%22start%22%2C%22label%22%3A%22a%22%2C%22stateB%22%3A%22s0%22%7D%2C%7B%22stateA%22%3A%22s0%22%2C%22label%22%3A%22b%22%2C%22stateB%22%3A%22s1%22%7D%2C%7B%22stateA%22%3A%22s1%22%2C%22label%22%3A%22c%22%2C%22stateB%22%3A%22s2%22%7D%2C%7B%22stateA%22%3A%22s2%22%2C%22label%22%3A%22d%22%2C%22stateB%22%3A%22s3%22%7D%5D%2C%22bulkTests%22%3A%7B%22accept%22%3A%22%22%2C%22reject%22%3A%22%22%7D%7D)

```cs
   var machine = new Automaton<States>()
           .StartIn(States.Q1)
           .EndIn(States.Q5)
              .When(States.Q1)
                  .On('a', States.Q2)
              .When(States.Q2)
                  .On('b', States.Q3)
              .When(States.Q3)
                  .On('c', States.Q4)
              .When(States.Q4)
                  .On('d', States.Q5)
              ;

```
obs. The on
After definition you can valid sentences:

```cs
 machine.Read("abcd"); // true
 machine.Read("abcXd"); // false
```
You can alse verify the result with `machine.Valid()`


* Defines a Pushdown Automanton


[PDA for number of a(w) = number of b(w)](http://automatonsimulator.com/#%7B%22type%22%3A%22PDA%22%2C%22pda%22%3A%7B%22transitions%22%3A%7B%22start%22%3A%7B%22%22%3A%7B%22%22%3A%5B%7B%22state%22%3A%22s0%22%2C%22stackPushChar%22%3A%22%22%7D%5D%7D%7D%2C%22s0%22%3A%7B%22a%22%3A%7B%22%22%3A%5B%7B%22state%22%3A%22s2%22%2C%22stackPushChar%22%3A%22%24%22%7D%5D%7D%2C%22b%22%3A%7B%22%22%3A%5B%7B%22state%22%3A%22s3%22%2C%22stackPushChar%22%3A%22%24%22%7D%5D%7D%7D%2C%22s2%22%3A%7B%22%22%3A%7B%22%22%3A%5B%7B%22state%22%3A%22s1%22%2C%22stackPushChar%22%3A%22a%22%7D%5D%7D%7D%2C%22s3%22%3A%7B%22%22%3A%7B%22%22%3A%5B%7B%22state%22%3A%22s1%22%2C%22stackPushChar%22%3A%22b%22%7D%5D%7D%7D%2C%22s1%22%3A%7B%22%22%3A%7B%22%24%22%3A%5B%7B%22state%22%3A%22s0%22%2C%22stackPushChar%22%3A%22%22%7D%5D%7D%2C%22b%22%3A%7B%22%22%3A%5B%7B%22state%22%3A%22s1%22%2C%22stackPushChar%22%3A%22b%22%7D%5D%2C%22a%22%3A%5B%7B%22state%22%3A%22s1%22%2C%22stackPushChar%22%3A%22%22%7D%5D%7D%2C%22a%22%3A%7B%22%22%3A%5B%7B%22state%22%3A%22s1%22%2C%22stackPushChar%22%3A%22a%22%7D%5D%2C%22b%22%3A%5B%7B%22state%22%3A%22s1%22%2C%22stackPushChar%22%3A%22%22%7D%5D%7D%7D%7D%2C%22startState%22%3A%22start%22%2C%22acceptStates%22%3A%5B%22s0%22%5D%7D%2C%22states%22%3A%7B%22start%22%3A%7B%7D%2C%22s0%22%3A%7B%22isAccept%22%3Atrue%2C%22top%22%3A251%2C%22left%22%3A225%7D%2C%22s2%22%3A%7B%22top%22%3A150%2C%22left%22%3A419%7D%2C%22s3%22%3A%7B%22top%22%3A428%2C%22left%22%3A400%7D%2C%22s1%22%3A%7B%22top%22%3A281%2C%22left%22%3A595%7D%7D%2C%22transitions%22%3A%5B%7B%22stateA%22%3A%22start%22%2C%22label%22%3A%22%CF%B5%2C%CF%B5%2C%CF%B5%22%2C%22stateB%22%3A%22s0%22%7D%2C%7B%22stateA%22%3A%22s0%22%2C%22label%22%3A%22a%2C%CF%B5%2C%24%22%2C%22stateB%22%3A%22s2%22%7D%2C%7B%22stateA%22%3A%22s0%22%2C%22label%22%3A%22b%2C%CF%B5%2C%24%22%2C%22stateB%22%3A%22s3%22%7D%2C%7B%22stateA%22%3A%22s2%22%2C%22label%22%3A%22%CF%B5%2C%CF%B5%2Ca%22%2C%22stateB%22%3A%22s1%22%7D%2C%7B%22stateA%22%3A%22s3%22%2C%22label%22%3A%22%CF%B5%2C%CF%B5%2Cb%22%2C%22stateB%22%3A%22s1%22%7D%2C%7B%22stateA%22%3A%22s1%22%2C%22label%22%3A%22%CF%B5%2C%24%2C%CF%B5%22%2C%22stateB%22%3A%22s0%22%7D%2C%7B%22stateA%22%3A%22s1%22%2C%22label%22%3A%22b%2C%CF%B5%2Cb%22%2C%22stateB%22%3A%22s1%22%7D%2C%7B%22stateA%22%3A%22s1%22%2C%22label%22%3A%22b%2Ca%2C%CF%B5%22%2C%22stateB%22%3A%22s1%22%7D%2C%7B%22stateA%22%3A%22s1%22%2C%22label%22%3A%22a%2C%CF%B5%2Ca%22%2C%22stateB%22%3A%22s1%22%7D%2C%7B%22stateA%22%3A%22s1%22%2C%22label%22%3A%22a%2Cb%2C%CF%B5%22%2C%22stateB%22%3A%22s1%22%7D%5D%2C%22bulkTests%22%3A%7B%22accept%22%3A%22%22%2C%22reject%22%3A%22%22%7D%7D)

```cs
 
 var machine = new Automaton<States>()
                .StartIn(States.Q0)
                .EndIn(States.Q0)
                .When(States.Q0)
                     .On('a', States.Q2, null, '$')
                     .On('b', States.Q3, null, '$')
                .When(States.Q1)
                     .On('b', States.Q1, null, 'b')
                     .On('a', States.Q1, null, 'a')
                     .On('b', States.Q1, 'a', null)
                     .On('a', States.Q1, 'b', null)
                     .On(States.Q0, '$', null)
                .When(States.Q2)
                    .On(States.Q1, null, 'a')
                .When(States.Q3)
                    .On(States.Q1, null, 'b')
              ;
```

The number of "a" had to be the same of "b":

```cs
 machine.Read("aaabbb"); // true
 machine.Read("babababa"); // true

 machine.Read("bbbba"); // false
 machine.Read("aabbb"); // false
```

You can alse verify the result with `machine.Valid()`

### Events
Can assign a event when the stack changes

```cs
 machine.OnStackPush += (e, o) => /* do something */;
 machine.OnStackPop += (e, o) => /* do something */;
```
