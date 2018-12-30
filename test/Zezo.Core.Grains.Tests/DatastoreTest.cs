using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Zezo.Core.Configuration;
using Zezo.Core.GrainInterfaces;

namespace Zezo.Core.Grains.Tests
{
    [Collection("Default")] // All tests in the same collection, prevents parallel runs
    public class DatastoreTest : BaseGrainTest
    {
        public DatastoreTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper) {}

        [Fact]
        public async Task Can_Create_Entity_with_Datastore()
        {
            var config = ParseConfig(@"
                <Project Id=""test"">
                    <Project.Pipeline>
                        <If Id=""if1"">
                            <If.Child>
                                <DummyStep Id=""dummy1"" BeforeStart=""10ms"" Working=""1000ms"" />
                            </If.Child>
                            <If.Condition>
                                <ScriptCondition Language=""csharp"">
                                    <![CDATA[
                                        var a = 1+1;
                                        var b = 2;
                                        return Datastores[""default""][""ImageFile""] == null;
                                    ]]>
                                </ScriptCondition>
                            </If.Condition>
                        </If>
                    </Project.Pipeline>
                    <Project.Datastores>
                        <SimpleStore Id=""default"">
                            <SimpleStore.Fields>
                                <FieldDef Id=""ImageFile"" Type=""String"" Nullable=""True"" />
                            </SimpleStore.Fields>
                        </SimpleStore>
                    </Project.Datastores>
                </Project>
            ") as ProjectNode;
            
            var e1 = await CreateSingleEntityProject(config);

            var dummy1 = await GetStepGrainById(e1, "dummy1");
            var ifNode = await GetStepGrainById(e1, "if1");

            using (var observer = new TestObserver(_testOutputHelper, GrainFactory))
            {
                await observer.ObserverStep(ifNode, "if1");
                await observer.ObserverStep(dummy1, "dummy1");
                
                Assert.Equal(StepStatus.Inactive, await ifNode.GetStatus());
                Assert.Equal(StepStatus.Inactive, await dummy1.GetStatus());

                // kick off
                await e1.Start();

                await observer.WaitUntilStatus("if1", s => s == StepStatus.Completed);
                Assert.Equal(StepStatus.Completed, await dummy1.GetStatus());
            }
        }
    }
}