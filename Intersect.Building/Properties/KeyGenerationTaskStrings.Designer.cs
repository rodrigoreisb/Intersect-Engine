﻿//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Intersect.Building.Properties {
    using System;
    
    
    /// <summary>
    ///   Uma classe de recurso de tipo de alta segurança, para pesquisar cadeias de caracteres localizadas etc.
    /// </summary>
    // Essa classe foi gerada automaticamente pela classe StronglyTypedResourceBuilder
    // através de uma ferramenta como ResGen ou Visual Studio.
    // Para adicionar ou remover um associado, edite o arquivo .ResX e execute ResGen novamente
    // com a opção /str, ou recrie o projeto do VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class KeyGenerationTaskStrings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal KeyGenerationTaskStrings() {
        }
        
        /// <summary>
        ///   Retorna a instância de ResourceManager armazenada em cache usada por essa classe.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Intersect.Building.Properties.KeyGenerationTaskStrings", typeof(KeyGenerationTaskStrings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Substitui a propriedade CurrentUICulture do thread atual para todas as
        ///   pesquisas de recursos que usam essa classe de recurso de tipo de alta segurança.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a .
        /// </summary>
        internal static string ErrorCreatingOutputDirectory {
            get {
                return ResourceManager.GetString("ErrorCreatingOutputDirectory", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Failed to read existing private key, regenerating..
        /// </summary>
        internal static string ErrorReadingPrivateKey {
            get {
                return ResourceManager.GetString("ErrorReadingPrivateKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Failed to write new private key..
        /// </summary>
        internal static string ErrorWritingPrivateKey {
            get {
                return ResourceManager.GetString("ErrorWritingPrivateKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Failed to write new public key..
        /// </summary>
        internal static string ErrorWritingPublicKey {
            get {
                return ResourceManager.GetString("ErrorWritingPublicKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a GenerateEachBuild is false and keys already exist in {0}, skipping..
        /// </summary>
        internal static string KeysAlreadyExist {
            get {
                return ResourceManager.GetString("KeysAlreadyExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a New keys successfully generated..
        /// </summary>
        internal static string KeysGenerated {
            get {
                return ResourceManager.GetString("KeysGenerated", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a KeySize must be a power of 2 above 1024 but received {0}..
        /// </summary>
        internal static string KeySizeInvalid {
            get {
                return ResourceManager.GetString("KeySizeInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Invalid or no OutputDirectory was specified..
        /// </summary>
        internal static string OutputDirectoryInvalid {
            get {
                return ResourceManager.GetString("OutputDirectoryInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a The existing private key is invalid, regenerating..
        /// </summary>
        internal static string PrivateKeyInvalid {
            get {
                return ResourceManager.GetString("PrivateKeyInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Public key was missing but the private key already exists, regenerating only the public key..
        /// </summary>
        internal static string PublicKeyMissing {
            get {
                return ResourceManager.GetString("PublicKeyMissing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Successfully regenerated the public key..
        /// </summary>
        internal static string PublicKeyRegenerated {
            get {
                return ResourceManager.GetString("PublicKeyRegenerated", resourceCulture);
            }
        }
    }
}
