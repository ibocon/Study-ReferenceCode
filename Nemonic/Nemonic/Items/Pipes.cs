using System;
using System.IO;
using System.IO.Pipes;
using System.Diagnostics;
using System.Threading;

namespace nemonic
{
    //TODO: 프로세스 간에 통신을 어떻게 구현할 수 있을까?
    public class Pipes
    {
        AnonymousPipeServerStream pipeServer;
        AnonymousPipeClientStream pipeClient;

        public Pipes(string pipeHandle)
        {
            pipeServer = new AnonymousPipeServerStream(PipeDirection.In, HandleInheritability.Inheritable);
            pipeClient = new AnonymousPipeClientStream(PipeDirection.Out, pipeHandle);
        }
    }
}

