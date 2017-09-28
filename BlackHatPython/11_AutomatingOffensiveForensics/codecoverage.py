# TODO: Make Sure to save the file in the main Immunity Debugger installation directory under the PyCommands folder
# finds every function in calc.exe and for each one sets a one-shot breakpoint.
# Immunity Debugger outputs the address of the function and then removes the breakpoint
from immlib import *


class cc_hook(LogBpHook):

    def __init__(self):

        LogBpHook.__init__(self)
        self.imm = Debugger()

    def run(self, regs):

        self.imm.log("%08x" % regs['EIP'], regs['EIP'])
        self.imm.deleteBreakpoint(regs['EIP'])

        return


def main(args):

    imm = Debugger()

    calc = imm.getModule("calc.exe")
    imm.analyseCode(calc.getCodebase())

    functions = imm.getAllFunctions(calc.getCodebase())

    hooker = cc_hook()

    for function in functions:
        hooker.add("%08x" % function, function)

    return "Tracking %d functions." % len(functions)
