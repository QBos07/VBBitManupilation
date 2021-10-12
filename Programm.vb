Imports System.IO
Imports BenchmarkDotNet.Attributes
Imports BenchmarkDotNet.Columns
Imports BenchmarkDotNet.Configs
Imports BenchmarkDotNet.Diagnosers
Imports BenchmarkDotNet.Environments
Imports BenchmarkDotNet.Exporters
Imports BenchmarkDotNet.Exporters.Csv
Imports BenchmarkDotNet.Exporters.Json
Imports BenchmarkDotNet.Jobs
Imports BenchmarkDotNet.Running

'<SimpleJob(RuntimeMoniker.Wasm)>
'<SimpleJob(RuntimeMoniker.WasmNet50)>
'<SimpleJob(RuntimeMoniker.WasmNet60)>
'<SimpleJob(RuntimeMoniker.MonoAOTLLVM)>
'<SimpleJob(RuntimeMoniker.Net50)>
'<SimpleJob(RuntimeMoniker.HostProcess)>
'<SimpleJob(RuntimeMoniker.Mono)>
'<SimpleJob(RuntimeMoniker.NetCoreApp31)>
'<SimpleJob(RuntimeMoniker.Net48, -1, -1, -1, -1, Nothing, True)>
'<SimpleJob(RuntimeMoniker.Net60)>
'<SimpleJob(RuntimeMoniker.Net472)>
'<MarkdownExporter, AsciiDocExporter, HtmlExporter, CsvExporter>
'<MinColumn, MaxColumn>
'<MemoryDiagnoser>
Public Class Programm

	Private Strs As String()
	Private Poss As Integer()
	Private Boos As Boolean()

	<Params(1, 10)>
	Public N As Integer

	<GlobalSetup>
	Public Sub Setup()
		Dim rand = New Random(Now.Ticks.GetHashCode)
		ReDim Strs(N)
		ReDim Poss(N)
		ReDim Boos(N)
		For index = 1 To N
			Strs(index - 1) = Path.GetRandomFileName()
			Poss(index - 1) = rand.Next(12)
			Boos(index - 1) = Convert.ToBoolean(rand.Next(1))
		Next
	End Sub

	<Benchmark>
	Public Sub StringGet()
		For index = 1 To N
			GetBit(Strs(index - 1), Poss(index - 1), False)
		Next
	End Sub

	<Benchmark>
	Public Sub CharArrayGet()
		For index = 1 To N
			GetBit(Strs(index - 1), Poss(index - 1), True)
		Next
	End Sub

	<Benchmark>
	Public Sub StringSetTrue()
		For index = 1 To N
			SetBit(Strs(index - 1), Poss(index - 1), True, False)
		Next
	End Sub

	<Benchmark>
	Public Sub CharArraySetTrue()
		For index = 1 To N
			SetBit(Strs(index - 1), Poss(index - 1), True, True)
		Next
	End Sub

	<Benchmark>
	Public Sub StringSetFalse()
		For index = 1 To N
			SetBit(Strs(index - 1), Poss(index - 1), False, False)
		Next
	End Sub

	<Benchmark>
	Public Sub CharArraySetFalse()
		For index = 1 To N
			SetBit(Strs(index - 1), Poss(index - 1), False, True)
		Next
	End Sub

	<Benchmark>
	Public Sub StringSetRand()
		For index = 1 To N
			SetBit(Strs(index - 1), Poss(index - 1), Boos(index - 1), False)
		Next
	End Sub

	<Benchmark>
	Public Sub CharArraySetRand()
		For index = 1 To N
			SetBit(Strs(index - 1), Poss(index - 1), Boos(index - 1), True)
		Next
	End Sub

	Shared Sub Main(args As String())
		Dim summary = BenchmarkRunner.Run(GetType(Programm),
										  DefaultConfig.Instance _
										  .AddJob(Job.Default.WithRuntime(ClrRuntime.Net48).WithPlatform(Platform.AnyCpu).WithJit(Jit.RyuJit)) _
										  .AddJob(Job.Default.WithRuntime(ClrRuntime.Net472).WithPlatform(Platform.AnyCpu).WithJit(Jit.RyuJit)) _
										  .AddJob(Job.Default.WithRuntime(ClrRuntime.Net48).WithPlatform(Platform.AnyCpu).WithJit(Jit.LegacyJit)) _
										  .AddJob(Job.Default.WithRuntime(ClrRuntime.Net472).WithPlatform(Platform.AnyCpu).WithJit(Jit.LegacyJit)) _
										  .AddJob(Job.Default.WithRuntime(CoreRuntime.Core50).WithPlatform(Platform.AnyCpu).WithJit(Jit.RyuJit)) _
										  .AddJob(Job.Default.WithRuntime(CoreRuntime.Core60).WithPlatform(Platform.AnyCpu).WithJit(Jit.RyuJit)) _
										  .AddJob(Job.Default.WithRuntime(CoreRuntime.Core31).WithPlatform(Platform.AnyCpu).WithJit(Jit.RyuJit)) _
										  .AddJob(Job.Default.WithRuntime(CoreRtRuntime.CoreRt50).WithPlatform(Platform.AnyCpu)) _
										  .AddJob(Job.Default.WithRuntime(CoreRtRuntime.CoreRt60).WithPlatform(Platform.AnyCpu)) _
										  .AddJob(Job.Default.WithRuntime(CoreRtRuntime.CoreRt31).WithPlatform(Platform.AnyCpu)) _
										  .AddJob(Job.Default.WithRuntime(MonoRuntime.Default).WithPlatform(Platform.AnyCpu).WithJit(Jit.Llvm)) _
										  .AddJob(Job.Default.WithRuntime(ClrRuntime.Net48).WithPlatform(Platform.X64).WithJit(Jit.RyuJit)) _
										  .AddJob(Job.Default.WithRuntime(ClrRuntime.Net472).WithPlatform(Platform.X64).WithJit(Jit.RyuJit)) _
										  .AddJob(Job.Default.WithRuntime(ClrRuntime.Net48).WithPlatform(Platform.X64).WithJit(Jit.LegacyJit)) _
										  .AddJob(Job.Default.WithRuntime(ClrRuntime.Net472).WithPlatform(Platform.X64).WithJit(Jit.LegacyJit)) _
										  .AddJob(Job.Default.WithRuntime(CoreRuntime.Core50).WithPlatform(Platform.X64).WithJit(Jit.RyuJit)) _
										  .AddJob(Job.Default.WithRuntime(CoreRuntime.Core60).WithPlatform(Platform.X64).WithJit(Jit.RyuJit)) _
										  .AddJob(Job.Default.WithRuntime(CoreRuntime.Core31).WithPlatform(Platform.X64).WithJit(Jit.RyuJit)) _
										  .AddJob(Job.Default.WithRuntime(CoreRtRuntime.CoreRt50).WithPlatform(Platform.X64)) _
										  .AddJob(Job.Default.WithRuntime(CoreRtRuntime.CoreRt60).WithPlatform(Platform.X64)) _
										  .AddJob(Job.Default.WithRuntime(CoreRtRuntime.CoreRt31).WithPlatform(Platform.X64)) _
										  .AddJob(Job.Default.WithRuntime(MonoRuntime.Default).WithPlatform(Platform.X64).WithJit(Jit.Llvm)) _
										  .AddJob(Job.Default.WithRuntime(ClrRuntime.Net48).WithPlatform(Platform.X86).WithJit(Jit.RyuJit)) _
										  .AddJob(Job.Default.WithRuntime(ClrRuntime.Net472).WithPlatform(Platform.X86).WithJit(Jit.RyuJit)) _
										  .AddJob(Job.Default.WithRuntime(ClrRuntime.Net48).WithPlatform(Platform.X86).WithJit(Jit.LegacyJit).WithBaseline(True)) _
										  .AddJob(Job.Default.WithRuntime(ClrRuntime.Net472).WithPlatform(Platform.X86).WithJit(Jit.LegacyJit)) _
										  .AddJob(Job.Default.WithRuntime(CoreRuntime.Core50).WithPlatform(Platform.X86).WithJit(Jit.RyuJit)) _
										  .AddJob(Job.Default.WithRuntime(CoreRuntime.Core60).WithPlatform(Platform.X86).WithJit(Jit.RyuJit)) _
										  .AddJob(Job.Default.WithRuntime(CoreRuntime.Core31).WithPlatform(Platform.X86).WithJit(Jit.RyuJit)) _
										  .AddJob(Job.Default.WithRuntime(CoreRtRuntime.CoreRt50).WithPlatform(Platform.X86)) _
										  .AddJob(Job.Default.WithRuntime(CoreRtRuntime.CoreRt60).WithPlatform(Platform.X86)) _
										  .AddJob(Job.Default.WithRuntime(CoreRtRuntime.CoreRt31).WithPlatform(Platform.X86)) _
										  .AddJob(Job.Default.WithRuntime(MonoRuntime.Default).WithPlatform(Platform.X86).WithJit(Jit.Llvm)) _
										  .AddDiagnoser(MemoryDiagnoser.Default) _
										  .AddColumn(StatisticColumn.Min) _
										  .AddColumn(StatisticColumn.Max) _
										  .AddExporter(MarkdownExporter.Atlassian, MarkdownExporter.GitHub, MarkdownExporter.StackOverflow, MarkdownExporter.Default, MarkdownExporter.Console) _
										  .AddExporter(AsciiDocExporter.Default, HtmlExporter.Default, CsvExporter.Default, PlainExporter.Default, JsonExporter.FullCompressed, JsonExporter.Brief) _
										  , args)
	End Sub
End Class
