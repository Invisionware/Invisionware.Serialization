# Invisiownare Serialization
This part of the Invisionware framework provides extensions, utilities, and enhancements to serilizing objects within .NET

## Serialization Providers

### JsonNET
This library adds support for a number of enhancements for the Json.NET library

#### ContractResolverDelegate
Adds support for resolving interfaces during deserialization using any IOC framework (ex: Invisionware.Ioc)

##### Usage
```
var jsonSettings = new Newtonsoft.Json.JsonSerializerSettings
{
	container = new Invisionware.IoC.Autofac.AutofacContainer(new Autofac.ContainerBuilder().Build());
	container.Register<IDependencyContainer>(container);

	Resolver.SetResolver(container.GetResolver());

	var jsonSettings = new Newtonsoft.Json.JsonSerializerSettings
	{
		ContractResolver = new ContractResolverDelegate(
				(type1) => container.GetResolver().IsRegistered(type1),
				(type2) =>
				{
					var c = container.GetResolver().Resolve(type2);

					return c;
				})
			,DateFormatString = DateTimeUtils.CurrentFormatString
			,DateFormatHandling = DateFormatHandling.IsoDateFormat
			,DateParseHandling = DateParseHandling.DateTimeOffset
			,TypeNameHandling = TypeNameHandling.None
			,ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			,NullValueHandling = NullValueHandling.Ignore
			,DefaultValueHandling = DefaultValueHandling.Ignore
			,Formatting = Formatting.Indented
		};
```

#### Serilziing Objects To/From Dictionaries
Adds support for serializing objects to dictionaries

##### Usage
```
public class CustomClass {
	public string Prop1 {get;set;}
	public string Prop2 {get;set;}
}

var obj = new CustomClass();

var d = obj.SerializeToDictionary()

Console.WriteLine(d["Prop1"]);
```

#### Enum Converters
Adds support for specifying custom names for enum values

##### Usage
```
public enum SampleTypes {
	[JsonEnum(Name = "Some Type 1")]
	Type1,
	[JsonEnum(Name = "Some Type 2")]
	Type2,
}
```

#### JsonSerializer

##### Usage
```
```

### ASPNet
This library adds support for serializing Media Types

### Net45

#### SystemJsonSerializer

##### Usage
```
```

#### SystemXmlSerializer

##### Usage
```
```
