﻿using FluentAssertions;
using Xunit;

namespace Automateles.Tests
{
    public class PushDownAutomatonTests
    {

        static Automaton<States> GetMachine()
        {

            //DPDA for number of a(w) = number of b(w)

            /*
            The DPDA definition
            http://automatonsimulator.com/#%7B%22type%22%3A%22PDA%22%2C%22pda%22%3A%7B%22transitions%22%3A%7B%22start%22%3A%7B%22%22%3A%7B%22%22%3A%5B%7B%22state%22%3A%22s0%22%2C%22stackPushChar%22%3A%22%22%7D%5D%7D%7D%2C%22s0%22%3A%7B%22a%22%3A%7B%22%22%3A%5B%7B%22state%22%3A%22s2%22%2C%22stackPushChar%22%3A%22%24%22%7D%5D%7D%2C%22b%22%3A%7B%22%22%3A%5B%7B%22state%22%3A%22s3%22%2C%22stackPushChar%22%3A%22%24%22%7D%5D%7D%7D%2C%22s2%22%3A%7B%22%22%3A%7B%22%22%3A%5B%7B%22state%22%3A%22s1%22%2C%22stackPushChar%22%3A%22a%22%7D%5D%7D%7D%2C%22s3%22%3A%7B%22%22%3A%7B%22%22%3A%5B%7B%22state%22%3A%22s1%22%2C%22stackPushChar%22%3A%22b%22%7D%5D%7D%7D%2C%22s1%22%3A%7B%22%22%3A%7B%22%24%22%3A%5B%7B%22state%22%3A%22s0%22%2C%22stackPushChar%22%3A%22%22%7D%5D%7D%2C%22b%22%3A%7B%22%22%3A%5B%7B%22state%22%3A%22s1%22%2C%22stackPushChar%22%3A%22b%22%7D%5D%2C%22a%22%3A%5B%7B%22state%22%3A%22s1%22%2C%22stackPushChar%22%3A%22%22%7D%5D%7D%2C%22a%22%3A%7B%22%22%3A%5B%7B%22state%22%3A%22s1%22%2C%22stackPushChar%22%3A%22a%22%7D%5D%2C%22b%22%3A%5B%7B%22state%22%3A%22s1%22%2C%22stackPushChar%22%3A%22%22%7D%5D%7D%7D%7D%2C%22startState%22%3A%22start%22%2C%22acceptStates%22%3A%5B%22s0%22%5D%7D%2C%22states%22%3A%7B%22start%22%3A%7B%7D%2C%22s0%22%3A%7B%22isAccept%22%3Atrue%2C%22top%22%3A251%2C%22left%22%3A225%7D%2C%22s2%22%3A%7B%22top%22%3A150%2C%22left%22%3A419%7D%2C%22s3%22%3A%7B%22top%22%3A428%2C%22left%22%3A400%7D%2C%22s1%22%3A%7B%22top%22%3A281%2C%22left%22%3A595%7D%7D%2C%22transitions%22%3A%5B%7B%22stateA%22%3A%22start%22%2C%22label%22%3A%22%CF%B5%2C%CF%B5%2C%CF%B5%22%2C%22stateB%22%3A%22s0%22%7D%2C%7B%22stateA%22%3A%22s0%22%2C%22label%22%3A%22a%2C%CF%B5%2C%24%22%2C%22stateB%22%3A%22s2%22%7D%2C%7B%22stateA%22%3A%22s0%22%2C%22label%22%3A%22b%2C%CF%B5%2C%24%22%2C%22stateB%22%3A%22s3%22%7D%2C%7B%22stateA%22%3A%22s2%22%2C%22label%22%3A%22%CF%B5%2C%CF%B5%2Ca%22%2C%22stateB%22%3A%22s1%22%7D%2C%7B%22stateA%22%3A%22s3%22%2C%22label%22%3A%22%CF%B5%2C%CF%B5%2Cb%22%2C%22stateB%22%3A%22s1%22%7D%2C%7B%22stateA%22%3A%22s1%22%2C%22label%22%3A%22%CF%B5%2C%24%2C%CF%B5%22%2C%22stateB%22%3A%22s0%22%7D%2C%7B%22stateA%22%3A%22s1%22%2C%22label%22%3A%22b%2C%CF%B5%2Cb%22%2C%22stateB%22%3A%22s1%22%7D%2C%7B%22stateA%22%3A%22s1%22%2C%22label%22%3A%22b%2Ca%2C%CF%B5%22%2C%22stateB%22%3A%22s1%22%7D%2C%7B%22stateA%22%3A%22s1%22%2C%22label%22%3A%22a%2C%CF%B5%2Ca%22%2C%22stateB%22%3A%22s1%22%7D%2C%7B%22stateA%22%3A%22s1%22%2C%22label%22%3A%22a%2Cb%2C%CF%B5%22%2C%22stateB%22%3A%22s1%22%7D%5D%2C%22bulkTests%22%3A%7B%22accept%22%3A%22%22%2C%22reject%22%3A%22%22%7D%7D

            */


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
            return machine;
        }

        [Theory()]
        [InlineData("aaabbb")]
        [InlineData("aabbbbaa")]
        [InlineData("aaabababbb")]
        [InlineData("babababa")]
        [InlineData("bbaababa")]
        [InlineData("")]
        public void TestPassPDA(string input)
        {
            var machine = GetMachine();

            machine.Read(input);

            machine.Valid().Should().BeTrue();

        }

        [Theory()]
        [InlineData("aab")]
        [InlineData("bba")]
        [InlineData("aabbb")]
        [InlineData("bbbba")]
        [InlineData("bbaab")]
        public void TestNotPassPDA(string input)
        {
            var machine = GetMachine();

            machine.Read(input);

            machine.Valid().Should().BeFalse();

        }

    }
}


