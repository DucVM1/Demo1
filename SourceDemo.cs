private string GetLangKeyFromRegistry()
        {
            // this variable use for save value which defined in Registry
            string regValue = string.Empty;

            // Registry KEY use to define config of Registry
            RegistryKey regKey = null;

            try
            {
                // Based on the operating system is 64bit, 32 bit ... to define regKEY value.
                if (Environment.Is64BitOperatingSystem)
                {
                    // this case is handle for Windows 64bit
                    regKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                }
                else
                {
                    // this case is handle for Windows 32bit and orther
                    regKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
                }

                // Subkey = Software\Rigaku\Windmax\Application\System\Language
                regKey = regKey.OpenSubKey(REGISTRY_SUBKEY_LANGUAGE);

                // parameter = sLanguage
                regValue = regKey.GetValue(REGISTRY_VALNAME_LANGUAGE).ToString();
            }

            // When read registry value, Exception occurred ArgumentNullException. Proceed to write log, and set Key Language to Empty
            catch (ArgumentNullException ex)
            {
                LogWriter.GetLogger().WriteLogError(string.Format("ArgumentNullException: {0}", ex.Message), this);
                regValue = string.Empty;
            }

            // When read registry value, Exception occurred UnauthorizedAccessException. Proceed to write log, and set Key Language to Empty
            catch (UnauthorizedAccessException ex)
            {
                LogWriter.GetLogger().WriteLogError(string.Format("UnauthorizedAccessException: {0}", ex.Message), this);
                regValue = string.Empty;
            }

            // When read registry value, Exception occurred ObjectDisposedException. Proceed to write log, and set Key Language to Empty
            catch (ObjectDisposedException ex)
            {
                LogWriter.GetLogger().WriteLogError(string.Format("ObjectDisposedException: {0}", ex.Message), this);
                regValue = string.Empty;
            }

            // When read registry value, Exception occurred SecurityException. Proceed to write log, and set Key Language to Empty
            catch (System.Security.SecurityException ex)
            {
                LogWriter.GetLogger().WriteLogError(string.Format("SecurityException: {0}", ex.Message), this);
                regValue = string.Empty;
            }

            // When read registry value, Exception occurred IOException. Proceed to write log, and set Key Language to Empty
            catch (System.IO.IOException ex)
            {
                LogWriter.GetLogger().WriteLogError(string.Format("IOException: {0}", ex.Message), this);
                regValue = string.Empty;
            }

            // When read registry value, Exception occurred Other Exception. Proceed to write log, and set Key Language to Empty
            catch (Exception ex)
            {
                LogWriter.GetLogger().WriteLogError(string.Format("Exception: {0}", ex.Message), this);
                regValue = string.Empty;
            }

            // return the value is being recorded in the registry at the sLanguage location. 
            return regValue;
        }

        /// <summary>
        /// Define the method of reading the System Language value from Region value
        /// </summary>
        /// <returns>Current System's Region Language</returns>
        private CultureInfo GetCultureCodeFromRegion()
        {
            // return system's region language
            return Thread.CurrentThread.CurrentCulture;
        }

        /// <summary>
        /// The method used for external classes takes the system culture information
        /// Determine the resource file of the language. 
        /// </summary>
        /// <returns>system culture information</returns>
        public static CultureInfo GetCurrentCulture()
        {
            // Return the pre-set culture value
            return GetLanguage().cultureInfo;
        }