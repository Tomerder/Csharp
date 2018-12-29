using System;
using System.IO;

namespace JackCompiler
{
    internal sealed class X86CodeGenerator : CodeGenerator
    {
        public override void EmitEnvironment()
        {
            throw new NotImplementedException();
        }

        public override void EmitBootstrapper()
        {
            throw new NotImplementedException();
        }

        public override void StaticDeclaration(Symbol variable)
        {
            throw new NotImplementedException();
        }

        public override void FieldDeclaration(Symbol variable)
        {
            throw new NotImplementedException();
        }

        public override void ConstructorDeclaration(Subroutine subroutine)
        {
            throw new NotImplementedException();
        }

        public override void FunctionDeclaration(Subroutine subroutine)
        {
            throw new NotImplementedException();
        }

        public override void MethodDeclaration(Subroutine subroutine)
        {
            throw new NotImplementedException();
        }

        public override void EndSubroutine()
        {
            throw new NotImplementedException();
        }

        public override void Return()
        {
            throw new NotImplementedException();
        }

        public override void BeginWhile()
        {
            //TODO: Generate label to which the loop can return

            throw new NotImplementedException();
        }

        public override void WhileCondition()
        {
            //TODO: Evaluate the condition and jump to the end of the loop

            throw new NotImplementedException();
        }

        public override void EndWhile()
        {
            //TODO: Emit the end-of-loop location

            throw new NotImplementedException();
        }

        public override void BeginIf()
        {
            //TODO: Evaluate the condition and branch to the optional 'else'
            //location (if there's no else, no biggie -- we will fall through
            //to the end-if location).

            throw new NotImplementedException();
        }

        public override void PossibleElse()
        {
            //TODO: Jump to the end-if location and emit a label for an
            //optional 'else' location.

            throw new NotImplementedException();
        }

        public override void EndIf()
        {
            //TODO: Emit the end-if location

            throw new NotImplementedException();
        }

        public override void Assignment(Token varName, bool withArrayIndex)
        {
            //TODO: Put the last value in the variable, if there's an array
            //index then it should be on the stack also (so that the stack
            //structure should be TOP --> LHS_VALUE --> ARR_INDEX).
            //Obtaining the array reference or the variable reference is the
            //responsibility of this method.

            throw new NotImplementedException();
        }

        public override void Add()
        {
            throw new NotImplementedException();
        }

        public override void Sub()
        {
            throw new NotImplementedException();
        }

        public override void Mul()
        {
            throw new NotImplementedException();
        }

        public override void Div()
        {
            throw new NotImplementedException();
        }

        public override void Mod()
        {
            throw new NotImplementedException();
        }

        public override void And()
        {
            throw new NotImplementedException();
        }

        public override void Or()
        {
            throw new NotImplementedException();
        }

        public override void Less()
        {
            throw new NotImplementedException();
        }

        public override void Greater()
        {
            throw new NotImplementedException();
        }

        public override void Equal()
        {
            throw new NotImplementedException();
        }

        public override void LessOrEqual()
        {
            throw new NotImplementedException();
        }

        public override void GreaterOrEqual()
        {
            throw new NotImplementedException();
        }

        public override void NotEqual()
        {
            throw new NotImplementedException();
        }

        public override void IntConst(int value)
        {
            throw new NotImplementedException();
        }

        public override void StrConst(string value)
        {
            throw new NotImplementedException();
        }

        public override void True()
        {
            throw new NotImplementedException();
        }

        public override void False()
        {
            throw new NotImplementedException();
        }

        public override void Null()
        {
            throw new NotImplementedException();
        }

        public override void This()
        {
            throw new NotImplementedException();
        }

        public override void Negate()
        {
            throw new NotImplementedException();
        }

        public override void Not()
        {
            throw new NotImplementedException();
        }

        public override void VariableRead(Token varName, bool withArrayIndex)
        {
            //TODO: The stack now has TOP --> ARRAY_INDEX or nothing.
            //It's this method's responsibility to obtain the array reference
            //or variable reference.

            throw new NotImplementedException();
        }

        public override void Call(string className, string subroutineName)
        {
            throw new NotImplementedException();
        }

        public override void DiscardReturnValueFromLastCall()
        {
            //Assuming that the we stored somewhere the return value from the
            //most recent subroutine call, now is the time to discard it (for
            //example, if it's stored on the stack).

            throw new NotImplementedException();
        }

        public override void BeginClass(string className)
        {
            throw new NotImplementedException();
        }

        public override void EndClass()
        {
            throw new NotImplementedException();
        }
    }
}