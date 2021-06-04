(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-5949ee1c"],{"01c1":function(t,e,i){},"166a":function(t,e,i){},"277e":function(t,e,i){"use strict";i("a9e3");var n=i("f977"),a=i("d9bd"),s=i("2b0e");e["a"]=s["a"].extend({name:"scrollable",directives:{Scroll:n["a"]},props:{scrollTarget:String,scrollThreshold:[String,Number]},data:function(){return{currentScroll:0,currentThreshold:0,isActive:!1,isScrollingUp:!1,previousScroll:0,savedScroll:0,target:null}},computed:{canScroll:function(){return"undefined"!==typeof window},computedScrollThreshold:function(){return this.scrollThreshold?Number(this.scrollThreshold):300}},watch:{isScrollingUp:function(){this.savedScroll=this.savedScroll||this.currentScroll},isActive:function(){this.savedScroll=0}},mounted:function(){this.scrollTarget&&(this.target=document.querySelector(this.scrollTarget),this.target||Object(a["c"])("Unable to locate element with identifier ".concat(this.scrollTarget),this))},methods:{onScroll:function(){var t=this;this.canScroll&&(this.previousScroll=this.currentScroll,this.currentScroll=this.target?this.target.scrollTop:window.pageYOffset,this.isScrollingUp=this.currentScroll<this.previousScroll,this.currentThreshold=Math.abs(this.currentScroll-this.computedScrollThreshold),this.$nextTick((function(){Math.abs(t.currentScroll-t.savedScroll)>t.computedScrollThreshold&&t.thresholdMet()})))},thresholdMet:function(){}}})},"3a66":function(t,e,i){"use strict";i.d(e,"a",(function(){return s}));var n=i("fe6c"),a=i("58df");function s(t){var e=arguments.length>1&&void 0!==arguments[1]?arguments[1]:[];return Object(a["a"])(Object(n["b"])(["absolute","fixed"])).extend({name:"applicationable",props:{app:Boolean},computed:{applicationProperty:function(){return t}},watch:{app:function(t,e){e?this.removeApplication(!0):this.callUpdate()},applicationProperty:function(t,e){this.$vuetify.application.unregister(this._uid,e)}},activated:function(){this.callUpdate()},created:function(){for(var t=0,i=e.length;t<i;t++)this.$watch(e[t],this.callUpdate);this.callUpdate()},mounted:function(){this.callUpdate()},deactivated:function(){this.removeApplication()},destroyed:function(){this.removeApplication()},methods:{callUpdate:function(){this.app&&this.$vuetify.application.register(this._uid,this.applicationProperty,this.updateApplication())},removeApplication:function(){var t=arguments.length>0&&void 0!==arguments[0]&&arguments[0];(t||this.app)&&this.$vuetify.application.unregister(this._uid,this.applicationProperty)},updateApplication:function(){return 0}}})}},"4c35":function(t,e,i){"use strict";i("01c1")},"604c":function(t,e,i){"use strict";i.d(e,"a",(function(){return l}));var n=i("5530"),a=(i("a9e3"),i("4de4"),i("caad"),i("2532"),i("a434"),i("159b"),i("fb6a"),i("7db0"),i("c740"),i("166a"),i("a452")),s=i("7560"),r=i("58df"),o=i("d9bd"),l=Object(r["a"])(a["a"],s["a"]).extend({name:"base-item-group",props:{activeClass:{type:String,default:"v-item--active"},mandatory:Boolean,max:{type:[Number,String],default:null},multiple:Boolean,tag:{type:String,default:"div"}},data:function(){return{internalLazyValue:void 0!==this.value?this.value:this.multiple?[]:void 0,items:[]}},computed:{classes:function(){return Object(n["a"])({"v-item-group":!0},this.themeClasses)},selectedIndex:function(){return this.selectedItem&&this.items.indexOf(this.selectedItem)||-1},selectedItem:function(){if(!this.multiple)return this.selectedItems[0]},selectedItems:function(){var t=this;return this.items.filter((function(e,i){return t.toggleMethod(t.getValue(e,i))}))},selectedValues:function(){return null==this.internalValue?[]:Array.isArray(this.internalValue)?this.internalValue:[this.internalValue]},toggleMethod:function(){var t=this;if(!this.multiple)return function(e){return t.internalValue===e};var e=this.internalValue;return Array.isArray(e)?function(t){return e.includes(t)}:function(){return!1}}},watch:{internalValue:"updateItemsState",items:"updateItemsState"},created:function(){this.multiple&&!Array.isArray(this.internalValue)&&Object(o["c"])("Model must be bound to an array if the multiple property is true.",this)},methods:{genData:function(){return{class:this.classes}},getValue:function(t,e){return null==t.value||""===t.value?e:t.value},onClick:function(t){this.updateInternalValue(this.getValue(t,this.items.indexOf(t)))},register:function(t){var e=this,i=this.items.push(t)-1;t.$on("change",(function(){return e.onClick(t)})),this.mandatory&&!this.selectedValues.length&&this.updateMandatory(),this.updateItem(t,i)},unregister:function(t){if(!this._isDestroyed){var e=this.items.indexOf(t),i=this.getValue(t,e);this.items.splice(e,1);var n=this.selectedValues.indexOf(i);if(!(n<0)){if(!this.mandatory)return this.updateInternalValue(i);this.multiple&&Array.isArray(this.internalValue)?this.internalValue=this.internalValue.filter((function(t){return t!==i})):this.internalValue=void 0,this.selectedItems.length||this.updateMandatory(!0)}}},updateItem:function(t,e){var i=this.getValue(t,e);t.isActive=this.toggleMethod(i)},updateItemsState:function(){var t=this;this.$nextTick((function(){if(t.mandatory&&!t.selectedItems.length)return t.updateMandatory();t.items.forEach(t.updateItem)}))},updateInternalValue:function(t){this.multiple?this.updateMultiple(t):this.updateSingle(t)},updateMandatory:function(t){if(this.items.length){var e=this.items.slice();t&&e.reverse();var i=e.find((function(t){return!t.disabled}));if(i){var n=this.items.indexOf(i);this.updateInternalValue(this.getValue(i,n))}}},updateMultiple:function(t){var e=Array.isArray(this.internalValue)?this.internalValue:[],i=e.slice(),n=i.findIndex((function(e){return e===t}));this.mandatory&&n>-1&&i.length-1<1||null!=this.max&&n<0&&i.length+1>this.max||(n>-1?i.splice(n,1):i.push(t),this.internalValue=i)},updateSingle:function(t){var e=t===this.internalValue;this.mandatory&&e||(this.internalValue=e?void 0:t)}},render:function(t){return t(this.tag,this.genData(),this.$slots.default)}});l.extend({name:"v-item-group",provide:function(){return{itemGroup:this}}})},8336:function(t,e,i){"use strict";var n=i("53ca"),a=i("3835"),s=i("5530"),r=(i("c7cd"),i("a9e3"),i("caad"),i("86cc"),i("10d2")),o=(i("99af"),i("8d4f"),i("90a2")),l=i("a9ad"),c=i("80d2"),u=l["a"].extend({name:"v-progress-circular",directives:{intersect:o["a"]},props:{button:Boolean,indeterminate:Boolean,rotate:{type:[Number,String],default:0},size:{type:[Number,String],default:32},width:{type:[Number,String],default:4},value:{type:[Number,String],default:0}},data:function(){return{radius:20,isVisible:!0}},computed:{calculatedSize:function(){return Number(this.size)+(this.button?8:0)},circumference:function(){return 2*Math.PI*this.radius},classes:function(){return{"v-progress-circular--visible":this.isVisible,"v-progress-circular--indeterminate":this.indeterminate,"v-progress-circular--button":this.button}},normalizedValue:function(){return this.value<0?0:this.value>100?100:parseFloat(this.value)},strokeDashArray:function(){return Math.round(1e3*this.circumference)/1e3},strokeDashOffset:function(){return(100-this.normalizedValue)/100*this.circumference+"px"},strokeWidth:function(){return Number(this.width)/+this.size*this.viewBoxSize*2},styles:function(){return{height:Object(c["f"])(this.calculatedSize),width:Object(c["f"])(this.calculatedSize)}},svgStyles:function(){return{transform:"rotate(".concat(Number(this.rotate),"deg)")}},viewBoxSize:function(){return this.radius/(1-Number(this.width)/+this.size)}},methods:{genCircle:function(t,e){return this.$createElement("circle",{class:"v-progress-circular__".concat(t),attrs:{fill:"transparent",cx:2*this.viewBoxSize,cy:2*this.viewBoxSize,r:this.radius,"stroke-width":this.strokeWidth,"stroke-dasharray":this.strokeDashArray,"stroke-dashoffset":e}})},genSvg:function(){var t=[this.indeterminate||this.genCircle("underlay",0),this.genCircle("overlay",this.strokeDashOffset)];return this.$createElement("svg",{style:this.svgStyles,attrs:{xmlns:"http://www.w3.org/2000/svg",viewBox:"".concat(this.viewBoxSize," ").concat(this.viewBoxSize," ").concat(2*this.viewBoxSize," ").concat(2*this.viewBoxSize)}},t)},genInfo:function(){return this.$createElement("div",{staticClass:"v-progress-circular__info"},this.$slots.default)},onObserve:function(t,e,i){this.isVisible=i}},render:function(t){return t("div",this.setTextColor(this.color,{staticClass:"v-progress-circular",attrs:{role:"progressbar","aria-valuemin":0,"aria-valuemax":100,"aria-valuenow":this.indeterminate?void 0:this.normalizedValue},class:this.classes,directives:[{name:"intersect",value:this.onObserve}],style:this.styles,on:this.$listeners}),[this.genSvg(),this.genInfo()])}}),h=u,d=i("4e82"),f=i("f2e7"),p=i("c995"),v=i("fe6c"),m=i("1c87"),b=i("af2b"),g=i("58df"),y=i("d9bd"),S=Object(g["a"])(r["a"],m["a"],v["a"],b["a"],Object(d["a"])("btnToggle"),Object(f["b"])("inputValue"));e["a"]=S.extend().extend({name:"v-btn",props:{activeClass:{type:String,default:function(){return this.btnToggle?this.btnToggle.activeClass:""}},block:Boolean,depressed:Boolean,fab:Boolean,icon:Boolean,loading:Boolean,outlined:Boolean,plain:Boolean,retainFocusOnClick:Boolean,rounded:Boolean,tag:{type:String,default:"button"},text:Boolean,tile:Boolean,type:{type:String,default:"button"},value:null},data:function(){return{proxyClass:"v-btn--active"}},computed:{classes:function(){return Object(s["a"])(Object(s["a"])(Object(s["a"])(Object(s["a"])(Object(s["a"])({"v-btn":!0},m["a"].options.computed.classes.call(this)),{},{"v-btn--absolute":this.absolute,"v-btn--block":this.block,"v-btn--bottom":this.bottom,"v-btn--disabled":this.disabled,"v-btn--is-elevated":this.isElevated,"v-btn--fab":this.fab,"v-btn--fixed":this.fixed,"v-btn--has-bg":this.hasBg,"v-btn--icon":this.icon,"v-btn--left":this.left,"v-btn--loading":this.loading,"v-btn--outlined":this.outlined,"v-btn--plain":this.plain,"v-btn--right":this.right,"v-btn--round":this.isRound,"v-btn--rounded":this.rounded,"v-btn--router":this.to,"v-btn--text":this.text,"v-btn--tile":this.tile,"v-btn--top":this.top},this.themeClasses),this.groupClasses),this.elevationClasses),this.sizeableClasses)},computedElevation:function(){if(!this.disabled)return p["a"].options.computed.computedElevation.call(this)},computedRipple:function(){var t,e=!this.icon&&!this.fab||{circle:!0};return!this.disabled&&(null!=(t=this.ripple)?t:e)},hasBg:function(){return!this.text&&!this.plain&&!this.outlined&&!this.icon},isElevated:function(){return Boolean(!this.icon&&!this.text&&!this.outlined&&!this.depressed&&!this.disabled&&!this.plain&&(null==this.elevation||Number(this.elevation)>0))},isRound:function(){return Boolean(this.icon||this.fab)},styles:function(){return Object(s["a"])({},this.measurableStyles)}},created:function(){var t=this,e=[["flat","text"],["outline","outlined"],["round","rounded"]];e.forEach((function(e){var i=Object(a["a"])(e,2),n=i[0],s=i[1];t.$attrs.hasOwnProperty(n)&&Object(y["a"])(n,s,t)}))},methods:{click:function(t){!this.retainFocusOnClick&&!this.fab&&t.detail&&this.$el.blur(),this.$emit("click",t),this.btnToggle&&this.toggle()},genContent:function(){return this.$createElement("span",{staticClass:"v-btn__content"},this.$slots.default)},genLoader:function(){return this.$createElement("span",{class:"v-btn__loader"},this.$slots.loader||[this.$createElement(h,{props:{indeterminate:!0,size:23,width:2}})])}},render:function(t){var e=[this.genContent(),this.loading&&this.genLoader()],i=this.generateRouteLink(),a=i.tag,s=i.data,r=this.hasBg?this.setBackgroundColor:this.setTextColor;return"button"===a&&(s.attrs.type=this.type,s.attrs.disabled=this.disabled),s.attrs.value=["string","number"].includes(Object(n["a"])(this.value))?this.value:JSON.stringify(this.value),t(a,this.disabled?s:r(this.color,s),e)}})},"86cc":function(t,e,i){},"8d4f":function(t,e,i){},bb51:function(t,e,i){"use strict";i.r(e);var n=function(){var t=this,e=t.$createElement,i=t._self._c||e;return i("div",[i("navigation",[i("router-view",{staticStyle:{"padding-bottom":"56px"}})],1),i("div",{staticClass:"menu"},[i("v-bottom-navigation",{attrs:{grow:"",value:t.activeTab,color:"primary"}},t._l(t.menuIcons,(function(e,n){return i("router-link",{key:n,attrs:{to:e.path,custom:""},scopedSlots:t._u([{key:"default",fn:function(n){var a=n.navigate;return[i("v-btn",{on:{click:a}},[i("span",[t._v(t._s(e.text))]),i("v-icon",[t._v(t._s(e.icon))])],1)]}}],null,!0)})})),1)],1)],1)},a=[],s=(i("c740"),i("402c")),r={name:"Home",data:function(){return{menuIcons:[{icon:s["b"].mdiBookshelf,text:"首页",path:"/"},{icon:s["b"].mdiBookshelf,text:"书架",path:"/bookshelf"},{icon:s["b"].mdiCog,text:"设置",path:"/setting"}]}},computed:{activeTab:function(){var t=this;return this.menuIcons.findIndex((function(e){return e.path===t.$route.path}))}}},o=r,l=(i("4c35"),i("2877")),c=i("6544"),u=i.n(c),h=i("5530"),d=(i("a9e3"),i("c7cd"),i("dd43"),i("3a66")),f=i("604c"),p=f["a"].extend({name:"button-group",provide:function(){return{btnToggle:this}},computed:{classes:function(){return f["a"].options.computed.classes.call(this)}},methods:{genData:f["a"].options.methods.genData}}),v=i("a9ad"),m=i("24b2"),b=i("a452"),g=i("277e"),y=i("7560"),S=i("f2e7"),x=i("58df"),V=i("d9bd"),O=Object(x["a"])(Object(d["a"])("bottom",["height","inputValue"]),v["a"],m["a"],Object(S["b"])("inputValue"),b["a"],g["a"],y["a"]).extend({name:"v-bottom-navigation",props:{activeClass:{type:String,default:"v-btn--active"},backgroundColor:String,grow:Boolean,height:{type:[Number,String],default:56},hideOnScroll:Boolean,horizontal:Boolean,inputValue:{type:Boolean,default:!0},mandatory:Boolean,shift:Boolean,tag:{type:String,default:"div"}},data:function(){return{isActive:this.inputValue}},computed:{canScroll:function(){return g["a"].options.computed.canScroll.call(this)&&(this.hideOnScroll||!this.inputValue)},classes:function(){return{"v-bottom-navigation--absolute":this.absolute,"v-bottom-navigation--grow":this.grow,"v-bottom-navigation--fixed":!this.absolute&&(this.app||this.fixed),"v-bottom-navigation--horizontal":this.horizontal,"v-bottom-navigation--shift":this.shift}},styles:function(){return Object(h["a"])(Object(h["a"])({},this.measurableStyles),{},{transform:this.isActive?"none":"translateY(100%)"})}},created:function(){this.$attrs.hasOwnProperty("active")&&Object(V["a"])("active.sync","value or v-model",this)},methods:{thresholdMet:function(){this.isActive=!this.isScrollingUp,this.$emit("update:input-value",this.isActive)},updateApplication:function(){return this.$el?this.$el.clientHeight:0},updateValue:function(t){this.$emit("change",t)}},render:function(t){var e=this.setBackgroundColor(this.backgroundColor,{staticClass:"v-bottom-navigation",class:this.classes,style:this.styles,props:{activeClass:this.activeClass,mandatory:Boolean(this.mandatory||void 0!==this.value),tag:this.tag,value:this.internalValue},on:{change:this.updateValue}});return this.canScroll&&(e.directives=e.directives||[],e.directives.push({arg:this.scrollTarget,name:"scroll",value:this.onScroll})),t(p,this.setTextColor(this.color,e),this.$slots.default)}}),w=i("8336"),B=i("132d"),k=Object(l["a"])(o,n,a,!1,null,"38a56e3d",null);e["default"]=k.exports;u()(k,{VBottomNavigation:O,VBtn:w["a"],VIcon:B["a"]})},dd43:function(t,e,i){},f977:function(t,e,i){"use strict";i.d(e,"a",(function(){return r}));var n=i("53ca");function a(t,e){var i=e.modifiers||{},a=i.self,s=void 0!==a&&a,r=e.value,o="object"===Object(n["a"])(r)&&r.options||{passive:!0},l="function"===typeof r||"handleEvent"in r?r:r.handler,c=s?t:e.arg?document.querySelector(e.arg):window;c&&(c.addEventListener("scroll",l,o),t._onScroll={handler:l,options:o,target:s?void 0:c})}function s(t){if(t._onScroll){var e=t._onScroll,i=e.handler,n=e.options,a=e.target,s=void 0===a?t:a;s.removeEventListener("scroll",i,n),delete t._onScroll}}var r={inserted:a,unbind:s};e["b"]=r}}]);