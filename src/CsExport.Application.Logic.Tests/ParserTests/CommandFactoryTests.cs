using CsExport.Application.Logic.Parser;
using CsExport.Application.Logic.Parser.Utility;
using Moq;
using Xunit;

namespace CsExport.Application.Logic.Tests.ParserTests
{
	public class CommandFactoryTests
	{
		private readonly ICommandFactory _commandFactory;

		private readonly Mock<IDependancyInjectionService> _dependancyInjectionServiceMock =
			new Mock<IDependancyInjectionService>();

		public CommandFactoryTests()
		{
			_commandFactory = new CommandFactory(_dependancyInjectionServiceMock.Object);
		}

		[Fact]
		public void Create_When_called_with_valid_arguments_Then_calls_dependancyInjectionServiceMock_resolve_with_valid_type()
		{
			var result = _commandFactory.Create(new StubArguments());

			_dependancyInjectionServiceMock.Verify(x => x.Resolve(typeof(ICommandWithArguments<StubArguments>)), Times.Once);
		}

		[Fact]
		public void Create_When_dependancyInjectionService_returns_null_Then_returns_null()
		{
			var result = _commandFactory.Create(new StubArguments());

			Assert.Null(result);
		}

		[Fact]
		public void Create_When_dependancyInjectionService_returns_command_Then_returns_not_null()
		{
			var commandMock = new Mock<ICommandWithArguments<StubArguments>>();
			_dependancyInjectionServiceMock.Setup(x => x.Resolve(typeof(ICommandWithArguments<StubArguments>)))
			                               .Returns(commandMock.Object);

			var result = _commandFactory.Create(new StubArguments());

			Assert.NotNull(result);
		}

		[Fact]
		public void Create_When_dependancyInjectionService_returns_command_Then_returns_command_with_correct_type()
		{
			var commandMock = new Mock<ICommandWithArguments<StubArguments>>();
			_dependancyInjectionServiceMock.Setup(x => x.Resolve(typeof(ICommandWithArguments<StubArguments>)))
			                               .Returns(commandMock.Object);
			var arguments = new StubArguments();

			var result = _commandFactory.Create(arguments);

			Assert.IsAssignableFrom<CompiledCommand<ICommandWithArguments<StubArguments>, StubArguments>>(result);
		}

		[Fact]
		public void
			Create_When_dependancyInjectionService_returns_command_Then_command_execute_calls_internal_command_execute_with_correct_arguments
			()
		{
			var commandMock = new Mock<ICommandWithArguments<StubArguments>>();
			_dependancyInjectionServiceMock.Setup(x => x.Resolve(typeof(ICommandWithArguments<StubArguments>)))
			                               .Returns(commandMock.Object);
			var arguments = new StubArguments();

			var result = _commandFactory.Create(arguments);
			result.Execute();

			commandMock.Verify(x => x.Execute(arguments));
		}


		public class StubArguments : IArguments
		{
		}
	}
}