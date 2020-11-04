# ListPagerRazorLibrary

This a set of Asp Net Core 3.0 View Components, Partial Views and related statics (css/js), styled with Bootstrap 4 
and wrapped in a Razor Class Library. It provides UI, paging event triggering and related paging-event scripting and is agnostic
about data source, sorting, etc. 

The Library uses vanilla javascript, e.g. no JQuery. 

See below for detailed description of library content and customization/override information.

## Setup

To use ListPager in your Asp.Net Core (3.0 or later) Web project :

- Add a reference to the **ListPagerRazorLibrary** project or DLL. 
   At this writing (10/2020) the library is **not available on NuGet**. 
   Download/ clone / fork the library and include it or it's DLL in your solution.

- In the host project's **\_ViewImports.cshtml** add these lines:

    ```
    @using ListPagerRazorLibrary
    @using ListPagerRazorLibrary.Models
    @using ListPagerRazorLibrary.ViewComponents
    ```

- In the host project's **\_Layout.cshtml** page include 

    ```
    <link rel="stylesheet" href="/_content/ListPagerRazorLibrary/css/pager.css" />
    ```
    or, one of the predefined themes

    ```
    <link rel="stylesheet" href="/_content/ListPagerRazorLibrary/css/pager-dark.css" />
    ```
    or
    ```
    <link rel="stylesheet" href="/_content/ListPagerRazorLibrary/css/pager-neon.css" />
    ```

## Examples

Please see the working examples in the examples project for implementation details in full page POST and GET scenarios, as well as different AJAX scenarios.

## Customizing & Overriding

For our purposes here, **overriding** refers to placing replacement files in the path that Dot Net expects to find them, entirely overriding the related library object, whether .cshtml or .css
while **customizing** refers altering appearance or behaviour without overriding an entire library file.

There are several ways to alter the pager's appearance and/or behaviour. These are demonstraed in the "examples' project but in summary, you can :

  - Use the **visibility toggles** and other settings available in the provided **ListPagerModel** c# Class (see below) 
      
  - **Override a library view** (e.g. 'ListPagerPageOf.cshtml') by adding your own version to your project (see path placement below)
  
  - **Override the CSS** in wwwroot/css/*.css' and/or include your own .css file(s)
  
  - **Change the layout**/ by composing your own container View Component, ignoring or overriding ListPager.cshtml / _ListPager.cshtml
      
### override file placement

At this writing RCL file-level overrides require that the host folder structure **mirror the library's structure**. 
ListPagerRazorLibrary has the following relevant structure:

  - **Pages/Shared**            (Partial View containers live here)

  - **Pages/Shared/Components** (all Component Views reside here)

  - **wwwroot/css/pager.css**   (base theme) 

  - **wwwroot/css/pager-dark.css** (darkness)

   - **wwwroot/css/pager-neon.css** (gloriously gaudy)

So, to override say, the ListPagerLinks.cshtml View Component with your own version, place your version in your project at:

    /pages/Shared/Components/ListPagerLinks.cshtml

Overriding **wwwroot/js/pager.js** will work but is not recommended. I'd suggest forking the project instead, 
unless you are comfortable handling any library future changes that impact the scripting.

## User CSS and Bootstrap

Whether you use the library's CSS files or override them, you can also include your own files as normal, 
to override specific selector definitions.

Note that the library uses Bootstrap .scss files, along with its own .scss, to transpile SASS into CSS, 
which means that you do not need to include the bootstrap's library CSS files in your pages, unless
you have broader need of them.

## CSS Classes

The RCL static resource "pager.css" (and the themed files) assigns some of its own class names:

- li.page-item
- li.page-item.disabled
- li.page-item.pager-range
- a.page-link
- a.page-link.disabled
- span.pager-page
- span.pager-dropdown

**If I've missed any please let me know**

## Partial Views In The Library
    
The library is primarily a View Component library and while any of the View Components can be invoked as 
Partial Views, the library also contains these "View Component free" Partial Views:

**_ListPagerShort**
* Implements the pager as a simple range (ul) of page numbers and elipses that provide movement up and down the full range of data pages.

**_ListPagerSheets**
* Implements the pager in a format suitable for paging through a document, for example a multi-page blog entry, with a fixed pager width.

**_ListPager**
* The fully composed ListPager that implements all of it's features as Partial Views, not View Components.


## ListPager View Components as Partial Views

With the caveats below you can also use any of the library's View Components as Partial Views.

For example like this for **ListPagerLinks.cshtml**:

    ```
    <partial name="Components/ListPagerLinks.cshtml" model="[ModelInstance]" />
                
    /* Note - ListPagerArrows and ListPagerDropdown require instances of their own Models,
    *  ListPagerArrowModel and ListPagerDropDownModel, and are therefore perhaps best 
    *  invoked as ViewComponents where the mode;ls are sintantiated automatically. 
    *
    *  All other View Components require a ListPagerModel instance which
    *  is typically passed to the container ListPager.cstml or _ListPager.cshtml
    *   
    *  See _ListPager.cshtml and ListPager.cshtml for example usage.
    */
    ```

## Library View Components

View components are provided for each major element of the pager, as well as for the default composition of the Pager itself.
        
**ListPagerArrows**

* Backward and Forward single and double arrows

* Note - requires an instance of **ListPagerArrowModel** which is automatically instantiated 
    when Invoked as a View Component but must be manually instantiated when used as a
    Partial View.


**ListPagerPageOf**

* The typical 'Page N of T' display

**ListPagerLinks**

* A series of N linked page numbers
* **ListPagerModel.MaxPageLinks** determines maximum N to be displayed

**ListPagerRecords**

* A 'N of T records' display, suitable for filtered data sets 
* Typically **ListPagerModel.IsFiltered** is in use with this

**ListPagerDropdown**
  
* A configurable drop-down menu of incremental page jumps 
  **ListPagerModel.DropDownIncrement** determines generated jump increments
  
* Note - requires an instance of ListPagerDropdownModel which is automatically instantiated
when Invoked as a View Component but must be manually instantiated when used as a
Partial View.

**ListPagerPageSize**

* A page-size UI element to allow user-setting of **ListPagerModel.PageSize**

**ListPagerPostForm**

* For convenience in POST scenarios, includes all **ListPagerModel** properties

**ListPager** 
* the pager itself comes in two flavours both of which can be overridden, typically to 
    change composition and/or Layout:

    * **Pages/Shared/Components/ListPager.cshtml** - uses the above views as View Components 
    * **Pages/Shared/_ListPager.cshtml** - uses the above views as Partial Views


## ListPagerModel C# Class

**ListPagerModel.cs** is required to implement the pager. See Examples for implemenation details. 
In summary it provides the following properties, and single method:

### Toggles

The following **ListPagerModel** Boolean properties are available to trigger display/non-display of individual 
View Components in the composed Pager.

**NOTE** that use of the toggles is manual - you must specify their values in your code and, 
if you are overriding **ListPager.cshtml** or composing your own pager, you must use them in 
conditional (if) statements within the composed ListPager to give them effect.

**ShowPageOf**
* Intended to toggle "ListPagerPageOf" display, default **false**

**ShowDropDown**
* Intended to toggle "ListPagerDropdown" display, default **false**

**ShowRecordsOf**
* Intended to  toggle "ListPagerRecords" display, default **false**

**ShowPageSize**
* Intended to toggle "ListPagerPageSize" display, default **true**


### Required Method

**CheckBoundaries()**

* The only method in the class, it is importantly responsible for checking/setting 
    page boundaries and **ListPagerModel.PageCount**.
    
* **IMPORTANT** - there is intentionally no other setter for **ListPagerModel.PageCount** as it is 
    **imperative** that you call this prior to returning data or a view.


### Other User-Settable Properties

**TotalRecords**
* Total unfiltered records in the dataset, as in: **myDataSet.Count()**
* **CheckBoundaries()** will set this to '0' if you leave it null

**MaxPageLinks**
* Maximum number of page-numbered links in the **ListPagerLinks.cshtml** view

**PageSize**
* Rows per page, used in the **ListPagerPageSize.cshtml** view 

**MaxPageSize**
* Maximum value for the Page Size ui input, enforced by the **ListPagerModel.PageSize** setter.

**MinPageSize**
* Minimum value for the Page Size ui input, enforced by the **ListPagerModel.PageSize** setter.

**PageSizeInputId**
* DOM id of the Page Size input box living in the **ListPagerPageSize** view

**PostTarget**
* The URL target to POST **ListPagerPostForm** to, not required if you are not using that form
     
**DropDownIncrement**
* The increment between page number links in the **ListPagerDropdown** view

**Sort State**
* for convenience if you are providing a single column sort, you can carry its state in these ListPagerModel props

    * **SortDirection**
    * **SortColumn**

## Other Properties

**PageCount**
* No public setter, this can only be set by calling the **CheckBoundaries()** method 

**PageNumber**
* Technically you can set this, but you should **always** ask **pager.js** instead, 
    via client Pager State, with **pager.goToPage(N)** which 
    fires a paging event. 

* **WARNING** - the pager.js **setActivePage(N)** method results in a direct UI update, no paging 
    event is issued. It may be deprecated in a future version.


**FilteredRecordCount**
* Defaults to TotalRecordCount if null. It impacts the **IsFiltered** flag which is typically
    used to determine **ListPagerModel.ShowRecordsOf** state (i.e. toggle the ListPagerRecords view)

**IsFiltered**
* No setter - Getter returns a comparison of the **ListPagerModel.TotalRecords** and 
    **ListPagerModel.FilteredRecordCount** property values

### Pull Requests and Maintenance 

Any errors or omissions please do let me know. 

I may or may not update this over time, but requests or suggestions are also welcome.

### Credits

Thanks to @ianbusko for this (Dot Net 2.0) head start: https://github.com/ianbusko/reusable-components-library
