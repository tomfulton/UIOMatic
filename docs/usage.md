# Usage #

With a db table and a petapoco poco in place you'll have a couple of steps to follow. Important: your table **must have a primary key column** (and your poco must mark it with the **PrimaryKeyColumn** attribute)


## Decorate your class with the UIOMatic attribute ##

In order for UI-O-Matic to pick up your poco you'll have to mark it with the *UIOMatic* attribute

	[UIOMatic("People","icon-users","icon-user")]

The UIOMatic attribute has a contructor with 3 parameters
	
- Name of the tree
- Icon used for the main tree node
- Icon used for the tree item nodes

You can also specify additional parameters

- ConnectionStringName, if you wish to use a different db then the current Umbraco one
- RenderType, if you wish to render the items in a listview or in the tree
- SortColumn, the default sort column
- SortOrder, the order of the sord (asc or desc) 

## Decorate properties with the UIOMaticField attribute ##

If your properties aren't marked with the *UIOMaticField* attribute, UI-O-Matic will display the property name as the label and also select a view based on the property type. If you wish to have more control you can mark your properties with the *UIOMaticField* attribute.

	 [UIOMaticField("Firstname","Enter your firstname")]

The attribute has a constructor with 2 parameters

- The name of the field (will be shown as the label for the field)
- The description of the field

Optionally it's also possible to specify a view

	[UIOMaticField("Picture", "Please select a picture",View ="file")]

There are a couple out of the box views you can use

- checkbox
- date
- datetime
- file
- label
- number
- password
- pickers.content
- pickers.media
- textarea
- textfield

Besides the out of the box ones you can also use a completely custom one 

 	[UIOMaticField("Owner", "Select the owner of the dog", View = "~/App_Plugins/Example/picker.person.html")]

## Decorate properties with the UIOMaticIgnoreField attribute ##

If you wish that UI-O-Matic ignores specific properties (so doesn't display then in the editor) you can mark those with the *UIOMaticIgnoreField* attribute

	[UIOMaticIgnoreField]

## Implement the IUIOMaticModel interface ##

Besides the attributes you also need to implement an interface, the *IUIOMaticModel* interface, it has a single member, the Validate method, this method will be used to validate your object when it is being saved. The method needs to return a list of Exceptions so it allows for complex validation rules.

        public IEnumerable<Exception> Validate()
        {
            var exs = new List<Exception>();

            if (string.IsNullOrEmpty(FirstName))
                exs.Add(new Exception("Please provide a value for first name"));

            if (string.IsNullOrEmpty(LastName))
                exs.Add(new Exception("Please provide a value for last name"));


            return exs;
        }

## Override the ToString method ##

UI-O-Matic will call the *ToString* method when it tries to fetch the tree item names, so make sure to override that one.

    public override string ToString()
    {
        return FirstName + " " + LastName;
    }

## Complete example ##
Here is a complete example that puts the different bits together.

    [UIOMatic("People", "icon-users", "icon-user")]
    [TableName("People")]
    public class Person : IUIOMaticModel
    {
        public Person() { }

        [UIOMaticIgnoreField]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [UIOMaticField("Firstname","Enter your firstname")]
        public string FirstName { get; set; }

        [UIOMaticField("Lastname", "Enter your lastname")]
        public string LastName { get; set; }

        [UIOMaticField("Picture", "Please select a picture",View ="file")]
        public string Picture { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }

        public IEnumerable<Exception> Validate()
        {
            var exs = new List<Exception>();

            if (string.IsNullOrEmpty(FirstName))
                exs.Add(new Exception("Please provide a value for first name"));

            if (string.IsNullOrEmpty(LastName))
                exs.Add(new Exception("Please provide a value for last name"));


            return exs;
        }
    }



