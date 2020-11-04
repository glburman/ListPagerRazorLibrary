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

**Overriding** refers to placing replacement files in the path that Dot Net expects to find them, entirely overriding the related library object, whether .cshtml or .css

**Customizing** refers altering appearance or behaviour without overriding an entire library file.

There are several ways to alter the pager's appearance and/or behaviour. Those are also demonstraed in the "examples' project but in summary, you can :

  - Use the **visibility toggles** and other settings available in the provided **ListPagerModel** c# Class (see below)
      
  - **Override a sub-view** (e.g. 'ListPagerPageOf.cshtml') by adding your own version to your project (see path placement below)
  
  - **Override the CSS** in wwwroot/css/*.css' and/or include your own .css file(s)
  
  - **Change the layout**/ by composing your own container View Component, ignoring or overriding ListPager.cshtml / _ListPager.cshtml
      
### override file placement

Any of the included views can be overridden by the host but at this writing RCL overrides require that the host folder structure **mirror the library's structure**. 

ListPagerRazorLibrary has the following relevant structure:

  - **Pages/Shared**            (Partial View containers live here)

  - **Pages/Shared/Components** (all Component Views reside here)

So, to override say, the ListPagerLinks.cshtml View Component with your own version, place your version in your project at:

    /pages/Shared/Components/ListPagerLinks.cshtml


## CSS and Javascript

CSS files placed on the appropriate path will also override the library's version. 

Whether you inlucde the library's CSS files or override them you can also include your own files as normal, 
to override specific selector values.

Note that the library uses Bootstrap .scss files, along with its own, to transpile SASS into CSS, 
which means that you do not need to include the bootstrap's library CSS files in your pages

Overriding **pager.js** will work but is not recommended. I'd suggest forking the project instead.

## CSS Classes

The RCL includes the static resource "pager.css" which provides default Bootstrap 4 styling. 
ListPager also assigns some of its own class names:

- li.page-item
- li.page-item.disabled
- li.page-item.pager-range
- a.page-link
- a.page-link.disabled
- span.pager-page
- span.pager-dropdown


## ListPager View Components as Partial Views

As noted there is a purely (internally all PV's) Partial View version of ListPager, _ListPager. 

With the caveats below you can also use any of the library's View Components as Partial Views as well.

For example like this for **ListPagerLinks.cshtml**:

    ```
    <partial name="Components/ListPagerLinks.cshtml" model="[ModelInstance]" />
                
    /* Note - ListPagerArrows and ListPagerDropdown require instances of their own Models,
    *  ListPagerArrowModel and ListPagerDropDownModel, and are therefore perhaps best 
    *  invoked as ViewComponents. All others require a ListPagerModel instance which
    *  is typically passed to the container ListPager.cstml or _ListPager.cshtml
    *  See _ListPager.cshtml and ListPager.cshtml for example usage.
    */
    ```

## View Components In The Library

View components are provided for each major element of the pager;
        
**ListPagerArrows**
    
    Backward and Forward single and double arrows
        
    Note - reqires an instance of ListPagerArrowModel which is automatically instantiated
    when Invoked as a View Component but must be manually instantiated when used as a
    Partial View.

**ListPagerPageOf**

    The typical 'Page N of T' display
    Toggle with 'ListPagerModel.ShowPageOf''

**ListPagerLinks**

    A series of N linked page numbers
    ListPagerModel.MaxPageLinks determines maximum N to be displayed

**ListPagerRecords**

    A 'N of T records' display, suitable for filtered data sets 
    Toggle with ListPagerModel.ShowRecordsOf 
    Typically ListPagerModel.IsFiltered is in use with this

**ListPagerDropdown**
  
    A configurable drop-down menu of incremental page jumps
    Toggle with "ListPagerModel.ShowDropDown"
    "ListPagerModel.DropDownIncrement" determines generated jump increments
      
    Note - reqires an instance of ListPagerDropdownModel which is automatically instantiated
    when Invoked as a View Component but must be manually instantiated when used as a
    Partial View.

**ListPagerPageSize**

    A page-size UI element to allow user-setting of ListPagerModel.PageSize
    Toggle with "ListPagerModel.ShowPageSize"

**ListPagerPostForm**

    For convenience in POST scenarios, includes all ListPagerModel properties

**ListPager** itself comes in two flavours both of which can be overridden, or ignored, typically to change Layout:

    Pages/Shared/Components/ListPager.cshtml and uses the above sub-views as View Components 

    Pages/Shared/_ListPager.cshtml and uses the above sub-views as Partial Views

## Partial Views In The Library
    
    While any of the library's View Components can be invoked as Partial Views, the library also contains these Partial Views;

**_ListPagerShort**
    
    Implements the pager as a simple range (ul) of page numbers and elipses that provide movement up and down the full range of data pages.

**_ListPagerSHeets**
    
    Implements the pager in a format suitable for paging through a document, for example a blog entry.

**_ListPager**

    The full blown ListPager that implements all of it's features as Partial Views


## ListPagerModel C# Class

ListPagerModel.cs is required to implement the pager. See Examples for implemenation details. 
In summary it provides the following method and properties:

**CheckBoundaries()**

    The only method in the class, it is importantly responsible for checking/setting 
    page boundaries and ListPagerModel.PageCount
     
    **There is intentionally no other setter for ListPagerModel.PageCount as it is 
    imperative that you call this prior to returning data or a view.**

**PageCount**

No public setter, this can only be set by calling the "CheckBoundaries()" method 

**PageNumber**

Always ask "pager.js" to set this, via client Pager State, with "pager.goToPage(N)" which 
fires a paging event. ** NOTE** that the JS "pager.setActivePage(N)" pager method is pure UI update, no paging 
event is issued and it may be deprecated in a future version.

**MaxPageLinks**

Maximum number of page-numbered links in the "ListPagerLinks" view

**TotalRecords**

Total unfiltered records in the dataset, as in: "myDataSet.Count()"
CheckBoundaries() will set this to '0' if you leave it null

**FilteredRecordCount**

Defaults to TotalRecordCount if null. It impacts the IsFiltered flag which is typically
used to determine ListPagerModel.ShowRecordsOf state (i.e. toggle the ListPagerRecords view)

**PageSize**

Rows per page, see the "ListPagerPageSize" view and "ListPagerModel.ShowPageSize" toggle property.

**MaxPageSize**

Maximum value for the Page Size ui input, enforced by the "ListPagerModel.PageSize" setter.

**MinPageSize**

Minimum value for the Page Size ui input, enforced by the "ListPagerModel.PageSize" setter.

**PageSizeInputId **

DOM id of the Page Size input box living in the "ListPagerPageSize" view

**PostTarget**

The URL target to POST "ListPagerPostForm" to, not required if you are not using that form
     
**DropDownIncrement**

The increment between page number links in the "ListPagerDropdown" view
     
**ShowPageOf**

Toggle "ListPagerPageOf" display, default **false**

**ShowDropDown**

Toggle "ListPagerDropdown" display, default **false**

**ShowRecordsOf**

Toggle "ListPagerRecords" display, default **false**

**ShowPageSize**

Toggle "ListPagerPageSize" display, default **true**

**IsFiltered**

No setter - Getter returns a comparison of the "ListPagerModel.TotalRecords" and 
"ListPagerModel.FilteredRecordCoount" property values

and for convenience 

**SortDirection**
**SortColumn**
    
If you are providing a single column sort, you can carry its state in those ListPagerModel props



### Pull Requests and Maintenance 

Any errors or omissions please do let me know. I may or may not update this over time, but requests or suggestions are also welcome.

### Credits

Thanks to @ianbusko for this head start: https://github.com/ianbusko/reusable-components-library
