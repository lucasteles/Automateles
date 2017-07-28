using FluentAssertions;
using Xunit;

namespace Automateles.Tests
{
    public class AutomatonTests
    {

        static Automaton<States> GetMachine()
        {
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

            return machine;
        }



        [Fact]
        public void ShouldPassWhenReadAValidText()
        {
            var machine = GetMachine();

            machine.Read("abcd");

            machine.Valid().Should().BeTrue();

        }


        [Fact]
        public void ShouldNotPassWhenReadATextWithOnlyRightPrefix()
        {
            var machine = GetMachine();

            machine.Read("abcdX");

            machine.Valid().Should().BeFalse();

        }


        [Fact]
        public void ShouldNotPassWhenReadAInvalidText()
        {
            var machine = GetMachine();

            machine.Read("abcXd");
            
            machine.Valid().Should().BeFalse();

        }


        [Fact]
        public void ShouldNotPassWhenReadAInvalidTextAndNotChangeState()
        {
            var machine = GetMachine();
            machine.Read("abcXd");

            machine.State.Should().Be(States.Q4);

        }


        [Fact]
        public void ShouldNotAcceptInANotFinalState()
        {
            var machine = GetMachine();
            machine.Read("abc");

            machine.Valid()
                 .Should().BeFalse();


        }



    }
}
