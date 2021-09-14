using System;
using System.Threading.Tasks;

using SharpC2.Screens;

namespace SharpC2.ScreenCommands
{
    public class SetHandlerParameterCommand : ScreenCommand
    {
        public override string Name => "set";
        public override string Description => "Set a Handler parameter";
        public override string Usage => "set [handler] [parameter] [value]";
        public override Screen.CommandCallback Callback => SetHandlerParameter;

        private readonly Screen _screen;

        public SetHandlerParameterCommand(Screen screen)
        {
            _screen = screen;
        }

        private async Task SetHandlerParameter(string[] args)
        {
            var handlerName = args[1];
            var paramName = args[2];
            var paramValue = args[3];

            var screen = (HandlersScreen)_screen;

            var handler = await screen.Api.SetHandlerParameter(handlerName, paramName, paramValue);
            
            var index = screen.Handlers.FindIndex(h =>
                h.Name.Equals(handlerName, StringComparison.OrdinalIgnoreCase));

            if (index == -1) screen.Handlers.Add(handler);
            else screen.Handlers[index] = handler;
        }
    }
}