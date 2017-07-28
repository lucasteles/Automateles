using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Automateles
{
    public class Automaton<T> where T : struct, IConvertible
    {


        private struct StateTransition<Ts>
        {
            public Ts State;
            public Expression<Func<char, bool>> Input;
            public object Push;
            public object Pop;

        };

        private readonly Stack<object> MainStack = new Stack<object>();
        private readonly Dictionary<T, List<StateTransition<T>>> Transitions = new Dictionary<T, List<StateTransition<T>>>();
        private readonly Dictionary<T, bool> HasEmptyTransitions = new Dictionary<T, bool>();
        private readonly List<T> FinalStates = new List<T>();
        public T State { get; private set; }
        private T? InitialState;
        private bool Started;
        private readonly List<T> TempWhen = new List<T>();
        public bool ReadAInvalidToken { get; private set; }
        public event EventHandler OnStackPush;
        public event EventHandler OnStackPop;
        private void DoOnStackPush(EventArgs e, object o) { if (OnStackPush != null)  OnStackPush(o, e); }
        private void DoOnStackPop(EventArgs e, object o) { if (OnStackPop != null)  OnStackPop(o, e); }

        public Automaton()
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");



        }

        public Automaton<T> StartIn(T state)
        {
            InitialState = state;
            return this;
        }

        public Automaton<T> EndIn(params T[] states)
        {
            FinalStates.AddRange(states);
            return this;
        }

        public Automaton<T> When(params T[] states)
        {
            TempWhen.Clear();
            foreach (var state in states)
            {
                if (Transitions.ContainsKey(state))
                    throw new ApplicationException("State already defined");

                Transitions.Add(state, new List<StateTransition<T>>());
                TempWhen.Add(state);
            }




            return this;
        }

        public Automaton<T> On(Expression<Func<char, bool>> input, T newState, object Pop = null, object Push = null)
        {
             var trasition = new StateTransition<T>
            {
                Input = input,
                Pop = Pop,
                Push = Push,
                State = newState
            };

            foreach (var state in TempWhen)
            {
                Transitions[state].Add(trasition);
                HasEmptyTransitions[state] = true;
            }
            return this;
        }

        public Automaton<T> On(char input, T newState, object Pop = null, object Push = null)
        {
            return On(e => e.Equals(input), newState, Pop, Push);
        }

        public Automaton<T> On(string input, T newState, object Pop = null, object Push = null)
        {
            foreach (var c in input)
                On(c, newState, Pop, Push);
            return this;
        }

        public Automaton<T> On(Regex regExp, T newState, object Pop = null, object Push = null)
        {
            return On(e => regExp.IsMatch(e.ToString()), newState, Pop, Push);
        }

        public Automaton<T> On(T newState, object Pop = null, object Push = null)
        {
            var trasition = new StateTransition<T>
            {
                Input = null,
                Pop = Pop,
                Push = Push,
                State = newState
            };

            foreach (var state in TempWhen)
            {
                Transitions[state].Add(trasition);
                HasEmptyTransitions[state] = true;
            }

            return this;
        }


        public void Reset()
        {
            MainStack.Clear();
            Started = false;
        }



        public bool Valid()
        {
            return FinalStates.Contains(State) && MainStack.Count == 0 && !ReadAInvalidToken;
        }

        private void StartMachine()
        {
          if (!Started)
            {
                if(!InitialState.HasValue)
                {
                    InitialState = Transitions.First().Key;
                }
                State = InitialState.Value;

                if (FinalStates.Count() == 0)
                    FinalStates.Add(Transitions.Last().Key);

                Started = true;


                DoEmptyTransitions();

            }
        }

        public bool Read(string input)
        {
            StartMachine();
            var ret = true;
          
            foreach (var c in input)
            {
                if (!(ret = Read(c)))
                    break;
            }
            DoEmptyTransitions();
            return ret;
        }
        public bool Read(char input)
        {

            StartMachine();


            if (!Transitions.ContainsKey(State) || ReadAInvalidToken)
            {
                ReadAInvalidToken = true;
                return false;
            } 
            var stateTrans = Transitions[State];
            stateTrans = stateTrans.OrderBy(e => e.Pop == null).ThenBy(e => e.Push == null).ToList();
            var ret = false;


            foreach (var t in stateTrans)
            {

                var validStack = !(t.Pop == null);
                var match = t.Input!=null && t.Input.Compile()(input);

                var noStack = true;
                if (t.Push != null && t.Pop != null)
                    noStack = !(t.Pop.Equals(t.Push));


                if (match)
                {
                    if (validStack)
                    {
                        if (MainStack.Count > 0 && MainStack.Peek().Equals(t.Pop))
                        {
                            if (noStack)
                                Pop();
                            State = t.State;
                            ret = true;
                        }
                        else
                        { continue; }
                    }
                    else
                    {
                        this.State = t.State;
                        ret = true;
                    }


                    if (!(t.Push == null) && noStack)
                        Push(t.Push);

                }


                if (ret)
                    break;

            }

            DoEmptyTransitions();
            if (!ret) // invalida resultados da maquina
            {
                MainStack.Push(null);
                ReadAInvalidToken = true;
            }

            return ret;
        }
        
       

        private void DoEmptyTransitions()
        {
            if (!(HasEmptyTransitions.ContainsKey(State) && HasEmptyTransitions[State]))
               return;

            // valid new empty transatcions

            if (!Transitions.ContainsKey(State))
                return;


            if (MainStack.Count > 0 && MainStack.Peek() == null)
                return;

            var stateTrans = Transitions[State];
            stateTrans = stateTrans.OrderBy(e => e.Pop==null).ThenBy(e=>e.Push==null).ToList();


            var changed = false;

            foreach (var t in stateTrans) {
                var validStack = !(t.Pop == null);
                var match = t.Input == null;
                var noStack = true;
                if (t.Push != null && t.Pop != null)
                    noStack = !(t.Pop.Equals(t.Push));

                if (match)
                {
                    if (validStack)
                    {
                        if (MainStack.Count > 0 && MainStack.Peek().Equals(t.Pop))
                        {
                            if (noStack)
                                Pop();

                            State = t.State;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        this.State = t.State;
                    }

                    if (!(t.Push == null) && noStack)
                        Push(t.Push);

                    changed = true;
                    break;
                }

            }

            if (changed && HasEmptyTransitions[State])
                DoEmptyTransitions();


        }


        public object Peek() { return MainStack.Count > 0 ? MainStack.Peek() : null; }
        public object Pop() {
            DoOnStackPop(EventArgs.Empty, this);
            return MainStack.Pop();
        }
        public void Push(object item) {
            MainStack.Push(item);
            DoOnStackPush(EventArgs.Empty, this);
        }
        public int StackLenght() { return MainStack.Count; }

    }


}
