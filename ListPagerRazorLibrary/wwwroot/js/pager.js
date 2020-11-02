export default class Pager {
    constructor(state, settings) {
        this.state = { ...state }
        this.settings = { ...settings }
    }

    static constants = {
        pagingEventName: 'PagerPagingEvent',
        pagingCompleteEventName: 'PagerPagingCompleteEvent',
        pageActiveClass: 'active',
        pageItemClass: 'page-item',
        pagerFormFieldClass: 'pager-value',
        pagerPostFormName: 'listPagerPostForm',
        pagerPageLinkIdPrefix: 'pager_li_'
    }

    postForm = (formName) => {
        const fname = formName ? formName : Pager.constants.pagerPostFormName
        const inputs = document[fname].getElementsByClassName(Pager.constants.pagerFormFieldClass)
        for (var i in inputs) {
            let input = inputs[i]
            if (input.name) {
                input.value = this.state[input.name]
            }
        }
        document[Pager.constants.pagerPostFormName].submit()
    }

    //credit: https://gist.github.com/lastguest/1fd181a9c9db0550a847
    static stateToUrlEncoded = (element, key, list) => {
        var list = list || []
        if (typeof (element) == 'object') {
            for (var idx in element)
                Pager.stateToUrlEncoded(element[idx], key ? key + '[' + idx + ']' : idx, list)
        } else {
            list.push(key + '=' + encodeURIComponent(element))
        }
        return list.join('&')
    }

    postEvent = async (e, url, afToken, state, target) => {
        if (e) {
            state.pager = { ...e.detail.ListPagerModel }
        }
        return await this.postState(url, afToken, state, target)
    }

    postState = async (url, afToken, state, target) => {
        const headers = new Headers()
        headers.append('RequestVerificationToken', afToken)
        headers.append('Content-Type', 'application/x-www-form-urlencoded')
        var body = Pager.stateToUrlEncoded(state)
        return await fetch(url, { method: "POST", body, headers })
            .then(response => {
                if (response.ok) {
                    if (target) {
                        response.text()
                            .then(text => {
                                document.getElementById(target).innerHTML = text
                            })
                    } else {
                        return response.json()
                    }
                } else {
                    console.log(response)
                    throw new Error('Pager.js Network response was not ok ')
                }
            })
            .catch((error) => {
                console.error('There has been a problem with your fetch operation:', error)
                throw (error.message)
            });
    }


    setSort = (column, direction) => {
        this.state.sortColumn = column
        this.state.sortDirection = direction
        this.goToPage(1)
    }

    setActivePage = (page) => {
        page = page === undefined ? 1 : page
        document.getElementById(Pager.constants.pagerPageLinkIdPrefix + page)
            .classList.add(this.settings.pageActiveClass)
    }

    setPageSize = (size) => {
        //ListPagerModel setter handles boundaries
        this.state.pageSize = parseInt(size)
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
