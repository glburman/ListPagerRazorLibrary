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
- Refer to the following example or the 'Examples' project for client-side (Javascript) setup/implementation, depending on your scenario.


## Example Host Page

This shows how to implement ListPager in a @page that uses Ajax to render a single Partial View 
containing both ListPager and the target List of data. 

See the **ListPagerExamples** project for other scenarios including an Ajax page with client-side rendering of the data List.

### MyPage.cshtml

here we need to:

- import the javascript **Pager** class
- initialize client pagerState using server-generated JSON
- instantiate ListPager with **page scope**
- handle the ListPager paging events to render/update the pager

```
    @page
    @model MyPageModel
    ...
    
    <script>
        // page scope
        let pager 
    </script>
    
   <script type="module">
        //path to ListPagerRazorLibrary
        import Pager from "../_content/ListPagerRazorLibrary/js/pager.js"  
        
        // initial pager state
        let pagerState = @Json.Serialize(Model.PagerModelInstance);
        
        //page scoped above  
        pager = new Pager(pagerState, {})

        const constants = {
            urlBase: "MyPage/partial",
            partialViewTarget: "partial-view-target",
            afTokenSelector: "input[name='__RequestVerificationToken']"
        }
        
        //we'll be good and handle the anti-forgery token as well'
        let afToken = null;
        
        //or whatever other doc-ready handler you prefer
        window.onload = () => {
        
            //Standard @Html.AntiForgeryToken() renders the token
            afToken = document.querySelectorAll(constants.afTokenSelector)[0].value;
            
            // Pager.js provides this async Fetch-API-based Ajax POST method, "postState" but
            // note that error handling is just catch-and-throw, so you can re-catch here or, 
            // if you want to use a another ajax library simply include it in your page and use it here.
            pager.postState(constants.urlBase, afToken, pagerState, constants.partialViewTarget)
        }
        
        // pager.js constants are static (Pager.constants not pager.constants)
        document.addEventListener(Pager.constants.pagingEventName, (e) => {
        
            // postEvent method extracts state from the Event object then calls pager.postState(...)
            // if you want to handle that outside of pager.js you'll need to implement it here
            pager.postEvent(e, constants.urlBase, afToken, pagerState, constants.partialViewTarget)
        
                // if the dom target parameter (constants.partialViewTarget) was null, this would be: 
                // .then(json =>{ // do things })
                .then(text => {
                    //otherwise we did it for you
                    console.log("this was inserted into the DOM at the target element->", text)
                })
        })
    </script>
    ...

    @Html.AntiForgeryToken()
    <div class="d-flex justify-content-center">
        <div id="partial-view-target">
        </div>
    </div>
    ...
```
### MyPage.cs
    
```
    ....

    public class MyPageModel : PageModel
    {
        private readonly AppDbContext _db;

        public MyPageModel(AppDbContext context)
        {
            PagerModelInstance = new ListPagerModel();
            _db = context;
        }
        
        //library conains the ListPagerModel class 
        public ListPagerModel PagerModelInstance {get; set;}
        
        //example only - pager is agnostic about data source
        public List<MyItemModel> MyPageData { get; set;}

        public void OnGet(){
            //nothing necessary until 'OnPostPartial' called by Ajax
		}

        public PartialViewResult OnPostPartial(ListPagerModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                GetPage(model);
                return new PartialViewResult
                {
                    ViewName = "_MyPartialView",
                    ViewData = new ViewDataDictionary<MyPageModel>(ViewData, this)
                };
            }
            // literal for clarity - use a resource string
            throw new ArgumentException("ListPagerModel invalid or null");
        }

        private void GetPage(ListPagerModel model){
            PagerModelInstance = model;
            
            var data = _db.Set<MyItemModel>();
            PagerModelInstance.TotalRecords = data.Count();
            
            // REQUIRED - this sets page count, row boundaries 
            PagerModelInstance.CheckBoundaries();

            int skip = PagerModelInstance.PageSize * (PagerModelInstance.PageNumber - 1);
            MyPageData = data
                        .Skip(skip)
                        .Take(PagerModelInstance.PageSize)
                        .ToList();
		}
        ...
    }
```

## Customizing & Overriding

**Overriding** refers to placing replacement files in the path that Dot Net expects to find them, entirely overriding the related library object, whether .cshtml or .css

**Customizing** refers altering appearance or behaviour withoout overriding the libraries files.

There are several ways to alter the pager's appearance and/or behaviour. In summary, you can :

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

For .CSS of .JS simply use your files as normal and do or do not also include the Library's, as appropriate


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


## ListPager as Partial View

As noted there is a purely (internally all PV's) Partial View version of ListPager, _ListPager 
but with the caveats below you can use any of the View Components as Partuial Views as well.

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

## Included View Components

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

## Included Partial Views
    
    While any of the library's View Components can be invoked as Partial Views, the library also contains these Partial Views;

**_ListPagerShort**
    
    Implements the pager as a simple range (ul) of page numbers and elipses that provide movement up and down the full range of data pages.

**_ListPagerSHeets**
    
    Implements the pager in a format suitable for paging through a document, for example a blog entry.

**_ListPager**

    The full blown ListPager that implements all of it's features as Partial Views


## ListPagerModel C# Class

ListPagerModel.cs provides the following method and properties:

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

Toggle "ListPagerPageOf" display

**ShowDropDown**

Toggle "ListPagerDropdown" display

**ShowRecordsOf**

Toggle "ListPagerRecords" display

**ShowPageSize**

Toggle "ListPagerPageSize" display

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
