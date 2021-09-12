using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using macheight.nba.dataaccess;
using macheight.nba.model;
using macheight.nba.service.Implementation;
using Moq;
using Xunit;

namespace macheight.nba.test
{
    public class CalculatorTest
    {
        private readonly Mock<IPlayerDataAccess> _playerDataAccessMock;
        private const string Path = "TestData";
        private Players _players;
        private Players _players_invalid_data;

        public CalculatorTest()
        {
            this._playerDataAccessMock = new Mock<IPlayerDataAccess>();

            Initialize();
        }

        [Fact]
        public async Task Get_Empty_Response_From_Service_Test()
        {
            // Given
            this._playerDataAccessMock
                .Setup(x => x.Get())
                .ReturnsAsync(new Players());

            const int input = 150;

            // When
            var calculator = new Calculator(this._playerDataAccessMock.Object);
            var result = await calculator.GetMatchAsync(input);

            // Then
            result.Should().BeEmpty();
            this._playerDataAccessMock.Verify(x => x.Get(), Times.Once());
        }

        [Fact]
        public async Task Get_Empty_Players_Response_From_Service_Test()
        {
            // Given
            this._playerDataAccessMock
                .Setup(x => x.Get())
                .ReturnsAsync(new Players
                {
                    Values = new List<Player>()
                });

            const int input = 150;

            // When
            var calculator = new Calculator(this._playerDataAccessMock.Object);
            var result = await calculator.GetMatchAsync(input);

            // Then
            result.Should().BeEmpty();
            this._playerDataAccessMock.Verify(x => x.Get(), Times.Once());
        }

        [Fact]
        public async Task Get_Players_Invalid_Data_From_Service_Exception_Test()
        {
            // Given
            this._playerDataAccessMock
                .Setup(x => x.Get())
                .ReturnsAsync(this._players_invalid_data);

            const int input = 150;

            // When
            var calculator = new Calculator(this._playerDataAccessMock.Object);
            Func<Task> func = async () => { await calculator.GetMatchAsync(input); };

            // Then
            _ = func.Should().ThrowAsync<Exception>()
                        .WithMessage("Input string was not in a correct format.");

        }

        [Fact]
        public async Task Get_Players_Success_Result_Input_150_Test()
        {
            // Given
            this._playerDataAccessMock
                .Setup(x => x.Get())
                .ReturnsAsync(this._players);

            const int input = 150;

            // When
            var calculator = new Calculator(this._playerDataAccessMock.Object);
            var result = await calculator.GetMatchAsync(input);

            // Then
            result.Should().NotBeEmpty();
            result.Count.Should().Be(13);
            result[0].Players1.Count.Should().Be(28);
            result[0].Players2.Count.Should().Be(13);
            result[1].Players1.Count.Should().Be(17);
            result[1].Players2.Count.Should().Be(21);
            result[2].Players1.Count.Should().Be(43);
            result[2].Players2.Count.Should().Be(2);
            result[3].Players1.Count.Should().Be(34);
            result[3].Players2.Count.Should().Be(13);
            result[4].Players1.Count.Should().Be(21);
            result[4].Players2.Count.Should().Be(17);
            result[5].Players1.Count.Should().Be(58);
            result[5].Players2.Count.Should().Be(1);
            result[6].Players1.Count.Should().Be(41);
            result[6].Players2.Count.Should().Be(2);
            result[7].Players1.Count.Should().Be(2);
            result[7].Players2.Count.Should().Be(41);
            result[8].Players1.Count.Should().Be(13);
            result[8].Players2.Count.Should().Be(34);
            result[9].Players1.Count.Should().Be(35);
            result[9].Players2.Count.Should().Be(35);
            result[10].Players1.Count.Should().Be(13);
            result[10].Players2.Count.Should().Be(28);
            result[11].Players1.Count.Should().Be(2);
            result[11].Players2.Count.Should().Be(43);
            result[12].Players1.Count.Should().Be(1);
            result[12].Players2.Count.Should().Be(58);
            this._playerDataAccessMock.Verify(x => x.Get(), Times.Once());
        }

        [Fact]
        public async Task Get_Players_Success_Result_Input_140_Test()
        {
            // Given
            this._playerDataAccessMock
                .Setup(x => x.Get())
                .ReturnsAsync(this._players);

            const int input = 140;

            // When
            var calculator = new Calculator(this._playerDataAccessMock.Object);
            var result = await calculator.GetMatchAsync(input);

            // Then
            result.Should().NotBeEmpty();
            result.Count.Should().Be(3);
            result[0].Players1.Count.Should().Be(2);
            result[0].Players2.Count.Should().Be(1);
            result[1].Players1.Count.Should().Be(2);
            result[1].Players2.Count.Should().Be(2);
            result[2].Players1.Count.Should().Be(1);
            result[2].Players2.Count.Should().Be(2);
            this._playerDataAccessMock.Verify(x => x.Get(), Times.Once());
        }

        [Fact]
        public async Task Get_Players_Success_Result_Input_139_Test()
        {
            // Given
            this._playerDataAccessMock
                .Setup(x => x.Get())
                .ReturnsAsync(this._players);

            const int input = 139;

            // When
            var calculator = new Calculator(this._playerDataAccessMock.Object);
            var result = await calculator.GetMatchAsync(input);

            // Then
            result.Should().NotBeEmpty();
            result.Count.Should().Be(2);
            result[0].Players1.Count.Should().Be(2);
            result[0].Players2.Count.Should().Be(1);
            result[1].Players1.Count.Should().Be(1);
            result[1].Players2.Count.Should().Be(2);
            this._playerDataAccessMock.Verify(x => x.Get(), Times.Once());
        }

        [Fact]
        public async Task Get_Players_Success_Result_Input_100_Test()
        {
            // Given
            this._playerDataAccessMock
                .Setup(x => x.Get())
                .ReturnsAsync(this._players);

            const int input = 100;

            // When
            var calculator = new Calculator(this._playerDataAccessMock.Object);
            var result = await calculator.GetMatchAsync(input);

            // Then
            result.Should().BeEmpty();
            result.Count.Should().Be(0);
            this._playerDataAccessMock.Verify(x => x.Get(), Times.Once());
        }

        private void Initialize()
        {
            this._players = FileHelper.GetFileContent(Path, "PlayerDataTest.json");
            this._players_invalid_data = FileHelper.GetFileContent(Path, "Player_Invalid_DataTest.json");
        }
    }
}
