(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-271b60d6"],{7819:function(e,t,n){},"950a":function(e,t,n){"use strict";n("7819")},ac7f:function(e,t,n){"use strict";n.r(t);var c=n("7a23"),o=function(e){return Object(c["K"])("data-v-f84f6c12"),e=e(),Object(c["I"])(),e},r={class:"header",ref:"header"},a=o((function(){return Object(c["l"])("div",null,[Object(c["m"])("阅读时长 "),Object(c["l"])("span",{style:{"font-size":"1.5em"}},"114514"),Object(c["m"])(" 小时")],-1)})),u=Object(c["m"])(" 打开书籍 "),i=Object(c["m"])(" 打开文件夹 "),b={class:"content"};function s(e,t,n,o,s,l){var d=Object(c["R"])("n-icon"),j=Object(c["R"])("n-button"),O=Object(c["R"])("n-button-group"),f=Object(c["R"])("n-popover"),p=Object(c["R"])("n-space"),k=Object(c["R"])("book-card"),g=Object(c["R"])("n-gi"),m=Object(c["R"])("book-group-card"),v=Object(c["R"])("n-grid");return Object(c["H"])(),Object(c["k"])("div",{class:"wrapper",style:Object(c["A"])(e.property)},[Object(c["l"])("div",r,[Object(c["n"])(p,{justify:"space-between",align:"center"},{default:Object(c["cb"])((function(){return[a,Object(c["l"])("div",null,[Object(c["n"])(p,null,{default:Object(c["cb"])((function(){return[Object(c["n"])(j,{text:""},{default:Object(c["cb"])((function(){return[Object(c["n"])(d,{size:"24",class:"icon"},{default:Object(c["cb"])((function(){return[Object(c["m"])(Object(c["U"])(e.icon.mdiMagnify),1)]})),_:1})]})),_:1}),Object(c["n"])(f,{placement:"bottom-end",trigger:"click",style:{padding:"0"}},{trigger:Object(c["cb"])((function(){return[Object(c["n"])(j,{text:""},{default:Object(c["cb"])((function(){return[Object(c["n"])(d,{size:"24",class:"icon"},{default:Object(c["cb"])((function(){return[Object(c["m"])(Object(c["U"])(e.icon.mdiDotsVertical),1)]})),_:1})]})),_:1})]})),default:Object(c["cb"])((function(){return[Object(c["n"])(O,{vertical:"",size:"large"},{default:Object(c["cb"])((function(){return[Object(c["n"])(j,{onClick:e.chooseBook},{default:Object(c["cb"])((function(){return[u]})),_:1},8,["onClick"]),Object(c["n"])(j,{onClick:e.chooseDir},{default:Object(c["cb"])((function(){return[i]})),_:1},8,["onClick"])]})),_:1})]})),_:1})]})),_:1})])]})),_:1})],512),Object(c["l"])("div",b,[Object(c["n"])(v,{"x-gap":"12","y-gap":"8",cols:3},{default:Object(c["cb"])((function(){var t;return[e.gid?(Object(c["H"])(!0),Object(c["k"])(c["b"],{key:0},Object(c["P"])(null===(t=e.bookList)||void 0===t?void 0:t.data,(function(e){return Object(c["H"])(),Object(c["i"])(g,{key:e.id},{default:Object(c["cb"])((function(){return[Object(c["n"])(k,{book:e},null,8,["book"])]})),_:2},1024)})),128)):(Object(c["H"])(!0),Object(c["k"])(c["b"],{key:1},Object(c["P"])(e.bookList,(function(e){return Object(c["H"])(),Object(c["i"])(g,{key:e.id},{default:Object(c["cb"])((function(){return["BookGroupCard"===e.type?(Object(c["H"])(),Object(c["i"])(m,{key:0,"book-list":e.data,id:e.id,"group-name":e.groupName},null,8,["book-list","id","group-name"])):(Object(c["H"])(),Object(c["i"])(k,{key:1,book:e.data},null,8,["book"]))]})),_:2},1024)})),128))]})),_:1})])],4)}var l=n("1da1"),d=(n("d81d"),n("4de4"),n("8a79"),n("4160"),n("159b"),n("96cf"),n("c872")),j=n("9381"),O=n("3519"),f=n("9a21"),p=n("646f"),k=n("2649"),g=n("5927"),m=n("a183");function v(){const e=Object(c["t"])(g["a"],null);return Object(c["g"])(()=>{if(null===e)return m["a"];const{mergedThemeRef:{value:t},mergedThemeOverridesRef:{value:n}}=e,c=(null===t||void 0===t?void 0:t.common)||m["a"];return(null===n||void 0===n?void 0:n.common)?Object.assign({},c,n.common):c})}var h=n("eaca"),y=n("cfe7"),R=n("7f6a"),w=n("6c02"),B=n("6022"),_=n("df7c"),x=Object(c["o"])({name:"BookShelf",props:{gid:String},components:{NSpace:d["a"],NButtonGroup:j["b"],NButton:O["a"],NPopover:f["a"],NGrid:p["a"],NGi:k["a"],BookGroupCard:y["b"],BookCard:y["a"]},setup:function(e){var t=v(),n=Object(w["d"])(),o=Object(R["a"])(),r=o.getBookList;o.init();var a=Object(c["t"])("chooseFile");return{bookList:Object(c["g"])((function(){return r(e.gid)})),icon:h["a"],property:Object(c["g"])((function(){return{"--border-size":"1px","--border-color":t.value.borderColor,"--border-radius":t.value.borderRadius,"--opacity-2":t.value.opacity2}})),chooseBook:function(){return Object(l["a"])(regeneratorRuntime.mark((function e(){var t,c;return regeneratorRuntime.wrap((function(e){while(1)switch(e.prev=e.next){case 0:return console.log("chooseBook"),e.next=3,a.chooseFile("epub");case 3:return t=e.sent,console.log(t),e.next=7,n.push({name:"Read",params:{path:t}});case 7:return e.next=9,Object(B["b"])(t);case 9:c=e.sent,o.getBookByPath(t)||o.addBook(c.id,{title:c.title,cover:c.cover,path:t});case 11:case"end":return e.stop()}}),e)})))()},chooseDir:function(){return Object(l["a"])(regeneratorRuntime.mark((function e(){var t,n;return regeneratorRuntime.wrap((function(e){while(1)switch(e.prev=e.next){case 0:return console.log("chooseDir"),e.next=3,a.chooseDir();case 3:return t=e.sent,n=function(){var e=Object(l["a"])(regeneratorRuntime.mark((function e(t){var c,r,a;return regeneratorRuntime.wrap((function(e){while(1)switch(e.prev=e.next){case 0:return c=[],e.next=3,Object(B["e"])(t);case 3:return r=e.sent,e.next=6,Object(B["a"])(t);case 6:a=e.sent,r.filter((function(e){return e.endsWith("epub")})).map((function(e){o.getBookByPath(e)||c.push(e)})),0!==c.length&&o.addBookGroup(_.basename(t),c.map((function(e){return{path:e}}))),a.forEach((function(e){n(e)}));case 10:case"end":return e.stop()}}),e)})));return function(t){return e.apply(this,arguments)}}(),e.next=7,n(t);case 7:case"end":return e.stop()}}),e)})))()}}}}),C=(n("950a"),n("6b0d")),H=n.n(C);const N=H()(x,[["render",s],["__scopeId","data-v-f84f6c12"]]);t["default"]=N}}]);