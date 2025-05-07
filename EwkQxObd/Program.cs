// See https://aka.ms/new-console-template for more information
using EwkQxObd;

int argLen = args.Length;

switch (argLen)
{
	case 0:
        Console.WriteLine(Prompts.ArgZero);
        Console.WriteLine(Prompts.HelpInfo);
		break;
	case 1:
        Console.WriteLine(Prompts.HelpInfo);
        break;
    case 2:

    default:
		break;
}