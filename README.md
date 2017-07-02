# Invisionware Framework
Invisionware Frmaework is a collection of utilities classes, extension methods, and new functionality to simplify creatig application in .NET. Amost all of the libraries are support on Dektop and Mobile (including Xamarin) development environments to provide the maxinum value possible.

## Serialization
This portion of the Invisionware Framework is focused on simplifying serialization

[![NuGet](https://img.shields.io/nuget/v/Invisionware.Serialization.svg)](https://www.nuget.org/packages/Invisionware.Serialization)

Packages related to Invisionware Serialziation
```powershell
Install-Package Invisionware.Serialization
```

Then just add the following using statement
```c#
using Invisionware.Serialization;
```

### Object Extensions
The following outline the extension methods provided for the system object class

#### IDictionary<string, string> SerializeToDictionary<T>(this T obj, DictionarySerializeOptions options = null) 
Creates a IDictionary<string, string> from the specified object

Note: QueryStringParamAttribute, JsonPropertyAttribute, and XmlElementAttribute (in that order) are all supproted for configuing 
the name of the parameter

**Review the Unit Tests for examples**

### MemberInfo Extensions
The following outline the extension methods provided for the MemberInfo class

#### static T GetCustomAttributeIncludingInterfaces<T>(this MemberInfo info) where T : Attribute
Gets the custom attribute from the memberinfo even if it is defined on one of the implemented interfaces.

```c#
var properties = obj.GetType().GetRuntimeProperties()
	.Where(x => x.GetCustomAttributeIncludingInterfaces<DictionaryElementAttribute>() != null);
```

### Converters
Adds support for additional converters for Newtonsoft Json.Net library

#### NewtonSoftJsonEnumConverter
TBD
