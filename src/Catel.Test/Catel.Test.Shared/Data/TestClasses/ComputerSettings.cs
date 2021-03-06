namespace Catel.Test.Data
{
    using System;
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;
    using Catel.Data;

    /// <summary>
    /// ComputerSettings Data object class which fully supports serialization, property changed notifications,
    /// backwards compatibility and error checking.
    /// </summary>
#if NET
    [Serializable]
#endif
    public class ComputerSettings : ComparableModelBase
    {
        #region Fields
        #endregion

        #region Constructors
        /// <summary>
        ///   Initializes a new object from scratch.
        /// </summary>
        public ComputerSettings()
        {
            IniFileCollection = InitializeDefaultIniFileCollection();
        }

#if NET
        /// <summary>
        ///   Initializes a new object based on <see cref = "SerializationInfo" />.
        /// </summary>
        /// <param name = "info"><see cref = "SerializationInfo" /> that contains the information.</param>
        /// <param name = "context"><see cref = "StreamingContext" />.</param>
        protected ComputerSettings(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
        #endregion

        #region Properties
        /// <summary>
        ///   Gets or sets the computer name.
        /// </summary>
        public string ComputerName
        {
            get { return GetValue<string>(ComputerNameProperty); }
            set { SetValue(ComputerNameProperty, value); }
        }

        /// <summary>
        ///   Register the property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ComputerNameProperty = RegisterProperty("ComputerName", typeof(string), string.Empty);

        /// <summary>
        ///   Gets or sets the collection of ini files.
        /// </summary>
        /// <remarks>
        ///   This type is an ObservableCollection{T} by purpose.
        /// </remarks>
        public ObservableCollection<IniFile> IniFileCollection
        {
            get { return GetValue<ObservableCollection<IniFile>>(IniFileCollectionProperty); }
            set { SetValue(IniFileCollectionProperty, value); }
        }

        /// <summary>
        ///   Register the property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IniFileCollectionProperty = RegisterProperty("IniFileCollection", typeof(ObservableCollection<IniFile>));
        #endregion

        #region Methods
        /// <summary>
        ///   Initializes the default ini file collection.
        /// </summary>
        /// <returns>New <see cref = "ObservableCollection{T}" />.</returns>
        private static ObservableCollection<IniFile> InitializeDefaultIniFileCollection()
        {
            var result = new ObservableCollection<IniFile>();

            // Add 3 files
            result.Add(ModelBaseTestHelper.CreateIniFileObject("Initial file 1", new[] { ModelBaseTestHelper.CreateIniEntryObject("G1", "K1", "V1") }));
            result.Add(ModelBaseTestHelper.CreateIniFileObject("Initial file 2", new[] { ModelBaseTestHelper.CreateIniEntryObject("G2", "K2", "V2") }));
            result.Add(ModelBaseTestHelper.CreateIniFileObject("Initial file 3", new[] { ModelBaseTestHelper.CreateIniEntryObject("G3", "K3", "V3") }));

            return result;
        }
        #endregion
    }
}