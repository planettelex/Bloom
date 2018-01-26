using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Enums;
using Bloom.Domain.Models;

namespace Bloom.Data.SeedData
{
    /// <summary>
    /// Seed data for time signatures.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISeedData{TimeSignature}" />
    public class TimeSignatureSeedData : ISeedData<TimeSignature>
    {
        public TimeSignatureSeedData()
        {
            _timeSignatures = new List<TimeSignature>
            {
                TwoTwo,
                TwoFour,
                ThreeFour,
                FourFour,
                FiveFour,
                SixFour,
                SevenFour,
                EightFour,
                FourEight,
                FiveEight,
                SixEight,
                SevenEight,
                EightEight,
                NineEight,
                TenEight,
                ElevenEight,
                TwelveEight
            };
        }
        private readonly List<TimeSignature> _timeSignatures;

        /// <summary>
        /// Lists the seed data for time signatures.
        /// </summary>
        /// <returns></returns>
        public List<TimeSignature> SeedData()
        {
            return _timeSignatures;
        }

        #region Time Signatures

        /// <summary>
        /// The 2/2 time signature.
        /// </summary>
        public TimeSignature TwoTwo => new TimeSignature
        {
            Id = Guid.Parse("8a3e33ef-1884-473c-9215-45a85d639d14"),
            BeatLength = BeatLength.Half,
            BeatsPerMeasure = 2
        };

        /// <summary>
        /// The 2/4 time signature.
        /// </summary>
        public TimeSignature TwoFour => new TimeSignature
        {
            Id = Guid.Parse("30bbd5f9-e3ec-4361-a6b6-c38336004560"),
            BeatLength = BeatLength.Quarter,
            BeatsPerMeasure = 2
        };

        /// <summary>
        /// The 3/4 time signature.
        /// </summary>
        public TimeSignature ThreeFour => new TimeSignature
        {
            Id = Guid.Parse("92bc12cc-2567-4d92-b608-04bc8af1d22a"),
            BeatLength = BeatLength.Quarter,
            BeatsPerMeasure = 3
        };

        /// <summary>
        /// The 4/4 time signature.
        /// </summary>
        public TimeSignature FourFour => new TimeSignature
        {
            Id = Guid.Parse("50327ebe-221e-4011-a8ec-0d6bb0058c26"),
            BeatLength = BeatLength.Quarter,
            BeatsPerMeasure = 4
        };

        /// <summary>
        /// The 5/4 time signature.
        /// </summary>
        public TimeSignature FiveFour => new TimeSignature
        {
            Id = Guid.Parse("f00b8118-beb3-4ec0-b591-3dd6f398f927"),
            BeatLength = BeatLength.Quarter,
            BeatsPerMeasure = 5
        };

        /// <summary>
        /// The 6/4 time signature.
        /// </summary>
        public TimeSignature SixFour => new TimeSignature
        {
            Id = Guid.Parse("2f9c5bc6-ef66-4aee-bbbc-a9660ba9c517"),
            BeatLength = BeatLength.Quarter,
            BeatsPerMeasure = 6
        };

        /// <summary>
        /// The 7/4 time signature.
        /// </summary>
        public TimeSignature SevenFour => new TimeSignature
        {
            Id = Guid.Parse("7237429c-5e3d-4e6c-8cf6-dbed714094f5"),
            BeatLength = BeatLength.Quarter,
            BeatsPerMeasure = 7
        };

        /// <summary>
        /// The 8/4 time signature.
        /// </summary>
        public TimeSignature EightFour => new TimeSignature
        {
            Id = Guid.Parse("54e59a3e-cdf9-4789-8fc9-4e1ab779ec9c"),
            BeatLength = BeatLength.Quarter,
            BeatsPerMeasure = 8
        };

        /// <summary>
        /// The 4/8 time signature.
        /// </summary>
        public TimeSignature FourEight => new TimeSignature
        {
            Id = Guid.Parse("f9bf0373-c878-4e07-95db-89211eed7700"),
            BeatLength = BeatLength.Eighth,
            BeatsPerMeasure = 4
        };

        /// <summary>
        /// The 5/8 time signature.
        /// </summary>
        public TimeSignature FiveEight => new TimeSignature
        {
            Id = Guid.Parse("69e9e795-01c7-4a7d-ae58-4021fa1ac3eb"),
            BeatLength = BeatLength.Eighth,
            BeatsPerMeasure = 5
        };

        /// <summary>
        /// The 6/8 time signature.
        /// </summary>
        public TimeSignature SixEight => new TimeSignature
        {
            Id = Guid.Parse("970fb3b3-6570-42c9-bb22-7348b36ae4eb"),
            BeatLength = BeatLength.Eighth,
            BeatsPerMeasure = 6
        };

        /// <summary>
        /// The 7/8 time signature.
        /// </summary>
        public TimeSignature SevenEight => new TimeSignature
        {
            Id = Guid.Parse("05ece8fd-e76a-4057-8ead-5cd4192984ed"),
            BeatLength = BeatLength.Eighth,
            BeatsPerMeasure = 7
        };

        /// <summary>
        /// The 8/8 time signature.
        /// </summary>
        public TimeSignature EightEight => new TimeSignature
        {
            Id = Guid.Parse("f2aac9df-d71b-4dd5-84f0-b04fd381ff85"),
            BeatLength = BeatLength.Eighth,
            BeatsPerMeasure = 8
        };

        /// <summary>
        /// The 9/8 time signature.
        /// </summary>
        public TimeSignature NineEight => new TimeSignature
        {
            Id = Guid.Parse("48202a18-9067-4287-8dff-e4b73a134183"),
            BeatLength = BeatLength.Eighth,
            BeatsPerMeasure = 9
        };

        /// <summary>
        /// The 10/8 time signature.
        /// </summary>
        public TimeSignature TenEight => new TimeSignature
        {
            Id = Guid.Parse("cb789854-36d1-44ee-a0f6-a92b24acba72"),
            BeatLength = BeatLength.Eighth,
            BeatsPerMeasure = 10
        };

        /// <summary>
        /// The 11/8 time signature.
        /// </summary>
        public TimeSignature ElevenEight => new TimeSignature
        {
            Id = Guid.Parse("1d8ffceb-aee1-42a9-8a37-c34246df11a8"),
            BeatLength = BeatLength.Eighth,
            BeatsPerMeasure = 11
        };

        /// <summary>
        /// The 12/8 time signature.
        /// </summary>
        public TimeSignature TwelveEight => new TimeSignature
        {
            Id = Guid.Parse("c46b5e8b-60a5-417f-9cba-97bf818951c1"),
            BeatLength = BeatLength.Eighth,
            BeatsPerMeasure = 12
        };

        #endregion
    }
}
