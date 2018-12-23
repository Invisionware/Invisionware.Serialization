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
A	// Set everything up
	// Get an instance to the dependency container (only needed it we want to setup a custom JsonSerializerSettings)
	container = Resolver.Resolve<IDependencyContainer>();

	// Setup the JsonSerializerSettings like we did above 
	jsonSettings = new ....
	var jsonSerializer = new JsonSeializer(jsonSettings);

	// Register it with the IoC framework
	container.Register<IJsonSerializer>(t => jsonSerializer);

	// Now if we need to get the serializer we can use the main resolver
	serializer = Resolver.Resolve(IJsonSerializer);

	// Lets serialize an object
	var str = serializer.Serialize(myCustomObject);

	// Lets deserialize it back into an object
	var obj = serializer.Deserialize<MyCustomObjectClass>(str);
```

### ASPNet
This library adds support for serializing Media Types

### Net45

#### SystemJsonSerializer

Uses the .NET 4.5 Desktop System.Runtime.Serialization.Json.DataContractJsonSerializer

##### Usage
```
	// Set everything up
	// Get an instance to the dependency container 
	container = Resolver.Resolve<IDependencyContainer>();

	var jsonSerializer = new SystemJsonSerializer();
	// Register it with the IoC framework
	container.Register<IJsonSerializer>(t => jsonSerializer);

	// Now if we need to get the serializer we can use the main resolver
	serializer = Resolver.Resolve(IJsonSerializer);

	// Lets serialize an object
	var str = serializer.Serialize(myCustomObject);

	// Lets deserialize it back into an object
	var obj = serializer.Deserialize<MyCustomObjectClass>(str);
````

#### SystemXmlSerializer
Uses the .NET 4.5 Desktop System.Runtime.Serialization.DataContractSerializer

##### Usage
```
	// Set everything up
	// Get an instance to the dependency container 
	container = Resolver.Resolve<IDependencyContainer>();

	var xmlSerializer = new SystemXmlSerializer();
	// Register it with the IoC framework
	container.Register<IXmlSerializer>(t => xmlSerializer);

	// Now if we need to get the serializer we can use the main resolver
	serializer = Resolver.Resolve(IXmlSerializer);

	// Lets serialize an object
	var str = serializer.Serialize(myCustomObject);

	// Lets deserialize it back into an object
	var obj = serializer.Deserialize<MyCustomObjectClass>(str);
```
