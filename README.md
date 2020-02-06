# ListPagerRazorLibrary

This a set of Asp Net Core 3.0 View Components, Partial Views with related statics (css/js), styled with Bootstrap 4 
and wrapped in a Razor Class Library. It provides UI, paging event triggering and related scripting and is agnostic
about data source, sorting, etc. 

See the **ListPagerExamples** project for working Ajax and non-Ajax usage.

## Setup

To use ListPager in your Asp.Net Core (3.0 or later) Web project :

- Add a reference to the **ListPagerRazorLibrary** project.

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

## The Host Page

This shows how to implement ListPager in a Host @page that uses Ajax to render a single Partial View 
containing both ListPager and the target List. See the **ListPagerExamples** project for other scenarios 
including an Ajax page with client-side rendering of the data List.

See **Customizing** below for overridding and other customization

### MyPage.cshtml

here we need to:

- import the javascript **Pager** class 
- initialize client pagerState as JSON
- instantiate ListPager with **page scope**
- handle the ListPager paging events to render the Partial View

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
        
        let afToken = null;
        window.onload = () => {
            // Html Helper renders this token
            afToken = document.querySelectorAll(constants.afTokenSelector)[0].value;
            
            // ListPager provides a set of Fetch API based Ajax POST methods
            pager.postState(constants.urlBase, afToken, pagerState, constants.partialViewTarget)
        }
        
        // ListPager constants are static (Pager.constants not pager.constants)
        document.addEventListener(Pager.constants.pagingEventName, (e) => {
        
            // postEvent extracts state from the Event object then calls pager.postState(...)
            pager.postEvent(e, constants.urlBase, afToken, pagerState, constants.partialViewTarget)
        
                // if the dom target parameter was null, this would be: .then(json =>{ // do things })
                .then(text => {
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
        
        public ListPagerModel PagerModelInstance {get; set;}
        
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
            throw new ArgumentException("ListPagerModel invalid or null");
        }

        private void GetPage(ListPagerModel model){
            PagerModelInstance = model;
            
            var data = _db.Set<MyItemModel>();
            PagerModelInstance.TotalRecords = data.Count();
            
            // REQUIRED
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

## Customizing

There are several ways to customize ListPager. In summary, you can :

  - Use the **visibility toggles** and other settings available in the provided **ListPagerModel** c# Class (see below)
      
  - **Override a sub-view** (e.g. 'ListPagerPageOf.cshtml') by adding your own version in the correct folder (see below)
  
  - **Override the CSS** in wwwroot/css/pager.css' and/or include your own .css file
  
  - **Change the pager layout**/ structure by using your own container View, ignoring or overriding ListPager.cshtml / _ListPager.cshtml
      
Any of the included views can be overridden by the host, as can the .css but at this writing RCL overrides require that the host folder structure mirror the library. **ListPagerRazorLibrary** has the following relevant structure:

  - **/css/pager.css**          (below wwwroot)

  - **/js/pager.js**            (might be better to fork the project than override this)

  - **Pages/Shared**            (only _ListPager.cshtml, the Partial View container, lives here)

  - **Pages/Shared/Components** (ListPager.cshtml and all the other Component Views)

Any of the included views can be used as Partial Views like this example for **ListPagerLinks.cshtml**:

    ```
    <partial name="Components/ListPagerLinks.cshtml" model="[ModelInstance]" />
                
    /* Note - ListPagerArrows and ListPagerDropdown require instances of their own Models,
    *  ListPagerArrowModel and ListPagerDropDownModel, and are therefore perhaps best 
    *  invoked as ViewComponents. All others require a ListPagerModel instance which
    *  is typically passed to the container ListPager.cstml or _ListPager.cshtml
    *  See _ListPager.cshtml and ListPager.cshtml for example usage.
    */
    ```

To alter Layout :

 - Override Listpager.cshtml or _ListPager.cshtml, or using your own container View instead.
 - Use the ListPager 'sub-views' and/or your own views/content in the HTML structure you wish
 
 
### Included Views

Views are provided for each major element of the pager. All can be used as **View Components or Partial Views**.
        
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
    Toggle with ListPagerModel.ShowRecordsOf and typically ListPagerModel.IsFiltered

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

    For convenience in POST scenarios

**ListPager** itself comes in two flavours both of which can be overridden, or ignored, typically to change Layout:

    Pages/Shared/Components/ListPager.cshtml and uses the above sub-views as View Components 

    Pages/Shared/_ListPager.cshtml and uses the above sub-views as Partial Views


### ListPagerModel Class

ListPagerModel.cs provides the following method and properties:

**CheckBoundaries()**

    The only method in the class, it is importantly responsible for checking/setting 
    page boundaries and ListPagerModel.PageCount
     
    There is intentionally no other setter for ListPagerModel.PageCount as it is 
    imperative that you call this prior to returning data or a view.

**PageCount**

No public setter, this can only be set by calling the "CheckBoundaries()" method 

**PageNumber**

Always ask "pager.js" to set this, via client Pager State, with "pager.goToPage(N)" which 
fires a paging event. The "pager.setActivePage(N)" pager method is pure UI update, no paging 
event is issued and it may be deprecated in a future version.

**MaxPageLinks**

Maximum number of page-numbered links in the "ListPagerLinks" view

**TotalRecords**

Total unfiltered records in the dataset, as in: "mySet.Count()"
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
    
If you are providing a single column sort, you can carry the state in ListPagerModel




### Styling

The RCL includes the static resource "pager.css" which provides default Bootstrap 4 styling. 
ListPager also assigns some of its own class names:

- li.page-item
- li.page-item.disabled
- li.page-item.pager-range
- a.page-link
- a.page-link.disabled
- span.pager-page
- span.pager-dropdown

### Pull Requests and Maintenance 

I may or may not update this over time, but requests or suggestions are welcome.

### Credits

Thanks to @ianbusko for this head start: https://github.com/ianbusko/reusable-components-library
