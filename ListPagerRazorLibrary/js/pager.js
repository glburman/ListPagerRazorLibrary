export default class Pager {
    constructor(state, settings) {
        this.state = { ...state }
        this.settings = { ...settings };
    }
    static constants = {
        pagingEventName: 'PagerPagingEvent',
        pagingCompleteEventName: 'PagerPagingCompleteEvent',
        pageActiveClass: 'active',
        pageItemClass: 'page-item',
        pagerFormFieldClass: 'pagerValue',
        pagerPostFormName: 'listPagerPostForm',
        pagerPageLinkIdPrefix: 'pager_li_'
    }

    post = () => {
        const inputs = document[Pager.constants.pagerPostFormName].getElementsByClassName(Pager.constants.pagerFormFieldClass)
        for (var i in inputs) {
            let input = inputs[i]
            if (input.name) {
                input.value = this.state[input.name]
            }
        }
        document[Pager.constants.pagerPostFormName].submit()
    }

    setSort = (column, direction) => {
        this.state.sortColumn = column
        this.state.sortDirection = direction
        this.goToPage(1);
    }
    setActivePage = (page) => {
        page = page === undefined ? 1 : page;
        document.getElementById(Pager.constants.pagerPageLinkIdPrefix + page).classList.add(this.settings.pageActiveClass)
    }

    setPageSize(size) {
        if (size < this.state.minSize) {
            this.state.pageSize = this.state.minPageSize
        }
        else if (size > this.state.maxPageSize) {
            this.state.pageSize = this.state.maxPageSize
        } else {
            this.state.pageSize = parseInt(size);
        }
        this.goToPage(1)
    }

    goToPage = (page) => {
        page = (page === null) ? this.state.pageNumber : page
        this.state.previousPage = this.state.pageNumber
        this.state.pageNumber = page
        document.dispatchEvent(
            new CustomEvent(Pager.constants.pagingEventName,
                { bubbles: false, detail: { ListPagerModel: { ...this.state } } }
            )
        )
    }
}
