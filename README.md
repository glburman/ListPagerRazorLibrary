# ListPagerRazorLibrary

This a set of Asp Net Core 3.0 View Components, Partial Views with related statics (css/js), styled with Bootstrap 4 
and wrapped in a Razor Class Library. It provides UI, paging event triggering and related scripting and is agnostic
about the data source, sorting, etc. 

It's relatively simple, but I've found it useful so thought I'd publish. As yet it's not available on Nuget. 

This is not a Blazor library.

## Included Views

Views are provided for each major element of the pager. The ListPager View uses the other view elements to render the Pager. 
Included are these View Components and, with one exception, corresponding Partial Views named using a leading underscore ("\_ViewComponentName").
        
    - ListPagerArrows
          Back and Forward single and double arrows 
          View Component Only, no Partial

    - ListPagerPageOf
          the typical 'Page N of T' display
          toggle with **ShowPageOf**

    - ListPagerLinks
          a series of N linked page numbers
          **MaxPagerLinks** determines maximum N to be displayed

    - ListPagerRecords
          a 'N of T records' display, suitable for filtered data sets 
          toggle with **ShowRecordsOf** and **IsFiltered**

    - ListPagerDropdown
          a configurable drop-down menu of incremental page jumps
          toggle with **ShowDropDown**
          **DropDownIncrement** determines generated jump increments

    - ListPagerPageSize
          a page-size UI element

    - ListPagerPostForm
          for convenience in POST scenarios

    - ListPager
          the outer View, containing the above 

## ListPagerModel

The **ListPagerModel** class (ListPagerModel.cs) provides the following method and properties:

     - CheckBoundaries()
            The only method in the class, it is responsible for setting **PageCount**. There is no 
            public **PageCount** setter so you **must** call this before returning your dataset or view.
            
     - PageCount 
            No public setter, this can only be set by calling the **CheckBoundaries()** method 

     - PageNumber 
            **_Always** ask **Pager.js** to set this, via client Pager State, with **pager.goToPage(N)** which 
            fires a paging event. The **pager.setActivePage(N)** pager method is pure UI update. No paging event 
            is issued. **It may be deprecated in a future version.**

     - MaxPagerLinks 
            Maximum number of page-numbered links in the **ListPagerLinks** views

     - TotalRecords
            Total unfiltered records in the dataset, as in: **mySet.Count()**
            CheckBoundaries() will set this to '0' if you leave it null

     - FilteredRecordCount 
            Defaults to TotalRecordCount if null. It impacts the IsFiltered flag which is typically
            used to determine whether the **ListPagerRecords** element should be displayed.

    - PageSize
            Rows per page, see the **ListPagerPageSize** view and **ShowPageSize** toggle property.

     - MaxPageSize
            Maximum value for the Page Size ui input, enforced by the **PageSize** setter.

     - MinPageSize
            Minimum value for the Page Size ui input, enforced by the **PageSize** setter.

     - PageSizeInputId 
            DOM id of the Page Size input box used in the **ListPagerPageSize** view

     - PostTarget
            The URL target to POST **ListPagerPostForm** to, not required if you are not using that form
            
     - DropDownIncrement
            The increment between page number links in the **ListPagerDropdown**
            
     - ShowPageOf
            Toggle **ListPagerPageOf** display

     - ShowDropDown
            Toggle **ListPagerDropdown** display

     - ShowRecordsOf
            Toggle **ListPagerRecords** display

     - ShowPageSize
            Toggle **ListPagerPageSize** display

     - IsFiltered 
            No setter - Getter returns a comparison of the **TotalRecords** and 
            **FilteredRecordCoount** property values

     and for convenience, if you are providing only single column sorts, you can carry the state in ListPagerModel
     
     - SortDirection
     - SortColumn

## Customizing

As normal with a Razor Class Library the host app can override any of the views to customize the pager. Note that : 

    - overriding the Partial View version (say _ListPagerPageOf) does not override the View Component version (ListPagerPageOf), and vice versa.
    - overriding an RCL view requires that the host project folder structure below **wwwroot** mirror the library's. See the Examples project.
    - folder names are case-sensitive for override purposes. "CSS" is not the same as "Css" or "css".

as well as View overrides, you can override **/css/pager.css** entirely or include a separate .css file that overrides specific selectors.

## Setup

To use ListPager in your Asp.Net Core Web project :

- add a reference to the **ListPagerRazorLibrary** project

- in **Startup.cs** add the following:

```    
     public void ConfigureServices(IServiceCollection services, ...)
     {
        ...
        services.AddListPagerViews();
        ...
     }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ....)
    {
        ....
        app.UseListPagerStatics();
        ....
    }
```

- in your project's **\_ViewImports.cshtml** add these lines:

```
  @using ListPagerRazorLibrary
  @using ListPagerRazorLibrary.Models
  @using ListPagerRazorLibrary.ViewComponents
```

- include **css/pager.css** in your **\_Layout.cshtml** page

- render ListPager in your view like this:

```
  @model ListPagerModel
  @await Component.InvokeAsync(nameof(ListPagerRazorLibrary.ViewComponents.ListPager), Model)
  
  // OR
   
  @model ListPagerModel
  <partial name="_ListPager" model=@Model/>
```

- fallback ListPagerModel defaults are set in the Constants.cs and ListPagerModel.cs files, but you'll likely want to set your own initial values like this:

```
    public void OnGet(int PageNumber=1, int PageSize = 5)
    {
        //...
            PagerData.PageNumber = PageNumber;
            PagerData.PageSize = PageSize;
            PagerData.TotalRecords = _db.Set<MyDataType>().Count();
            
            // ** !important! ** 
            // must do this to set PageCount
            PagerData.CheckBoundaries();  

    }
```

## Scripting

The library includes the static resource **Pager.js** (< 2 kb) which exports the default class 'Pager'. It must be Imported as a module.
Please refer to the **ListPagerExamples** project for use in various cases.

## Styling

The RCL includes the static resource **Pager.css** which provides default Bootstrap 4 styling. The remaninder of the styling is handled by Bootstrap. 
ListPager also assigns some class names for its own specific use:

- li.page-item
- li.page-item.disabled
- li.page-item.pager-range
- a.page-link
- a.page-link.disabled
- span.pager-page
- span.pager-dropdown

## Pull Requests and Maintenance 

I may or may not update this over time, but requests (or suggestions) are welcome, with the exception of requests for a Blazor implementation.
Fork away if Blazor is in your future.

## Credits

Thanks to @ianbusko for this head start: https://github.com/ianbusko/reusable-components-library
