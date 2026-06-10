; ModuleID = 'marshal_methods.armeabi-v7a.ll'
source_filename = "marshal_methods.armeabi-v7a.ll"
target datalayout = "e-m:e-p:32:32-Fi8-i64:64-v128:64:128-a:0:32-n32-S64"
target triple = "armv7-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [116 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [232 x i32] [
	i32 42639949, ; 0: System.Threading.Thread => 0x28aa24d => 108
	i32 67008169, ; 1: zh-Hant\Microsoft.Maui.Controls.resources => 0x3fe76a9 => 33
	i32 72070932, ; 2: Microsoft.Maui.Graphics.dll => 0x44bb714 => 50
	i32 98325684, ; 3: Microsoft.Extensions.Diagnostics.Abstractions => 0x5dc54b4 => 40
	i32 117431740, ; 4: System.Runtime.InteropServices => 0x6ffddbc => 101
	i32 165246403, ; 5: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 55
	i32 182336117, ; 6: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 73
	i32 195452805, ; 7: vi/Microsoft.Maui.Controls.resources.dll => 0xba65f85 => 30
	i32 199333315, ; 8: zh-HK/Microsoft.Maui.Controls.resources.dll => 0xbe195c3 => 31
	i32 205061960, ; 9: System.ComponentModel => 0xc38ff48 => 85
	i32 221958352, ; 10: Microsoft.Extensions.Diagnostics.dll => 0xd3ad0d0 => 39
	i32 280992041, ; 11: cs/Microsoft.Maui.Controls.resources.dll => 0x10bf9929 => 2
	i32 291275502, ; 12: Microsoft.Extensions.Http.dll => 0x115c82ee => 41
	i32 317674968, ; 13: vi\Microsoft.Maui.Controls.resources => 0x12ef55d8 => 30
	i32 318968648, ; 14: Xamarin.AndroidX.Activity.dll => 0x13031348 => 51
	i32 336156722, ; 15: ja/Microsoft.Maui.Controls.resources.dll => 0x14095832 => 15
	i32 342366114, ; 16: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 62
	i32 356389973, ; 17: it/Microsoft.Maui.Controls.resources.dll => 0x153e1455 => 14
	i32 379916513, ; 18: System.Threading.Thread.dll => 0x16a510e1 => 108
	i32 385762202, ; 19: System.Memory.dll => 0x16fe439a => 92
	i32 395744057, ; 20: _Microsoft.Android.Resource.Designer => 0x17969339 => 34
	i32 435591531, ; 21: sv/Microsoft.Maui.Controls.resources.dll => 0x19f6996b => 26
	i32 442565967, ; 22: System.Collections => 0x1a61054f => 82
	i32 450948140, ; 23: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 61
	i32 469710990, ; 24: System.dll => 0x1bff388e => 111
	i32 498788369, ; 25: System.ObjectModel => 0x1dbae811 => 98
	i32 500358224, ; 26: id/Microsoft.Maui.Controls.resources.dll => 0x1dd2dc50 => 13
	i32 503918385, ; 27: fi/Microsoft.Maui.Controls.resources.dll => 0x1e092f31 => 7
	i32 513247710, ; 28: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 45
	i32 539058512, ; 29: Microsoft.Extensions.Logging => 0x20216150 => 42
	i32 592146354, ; 30: pt-BR/Microsoft.Maui.Controls.resources.dll => 0x234b6fb2 => 21
	i32 627609679, ; 31: Xamarin.AndroidX.CustomView => 0x2568904f => 59
	i32 627931235, ; 32: nl\Microsoft.Maui.Controls.resources => 0x256d7863 => 19
	i32 662205335, ; 33: System.Text.Encodings.Web.dll => 0x27787397 => 105
	i32 672442732, ; 34: System.Collections.Concurrent => 0x2814a96c => 80
	i32 688181140, ; 35: ca/Microsoft.Maui.Controls.resources.dll => 0x2904cf94 => 1
	i32 706645707, ; 36: ko/Microsoft.Maui.Controls.resources.dll => 0x2a1e8ecb => 16
	i32 709557578, ; 37: de/Microsoft.Maui.Controls.resources.dll => 0x2a4afd4a => 4
	i32 722857257, ; 38: System.Runtime.Loader.dll => 0x2b15ed29 => 102
	i32 759454413, ; 39: System.Net.Requests => 0x2d445acd => 96
	i32 775507847, ; 40: System.IO.Compression => 0x2e394f87 => 89
	i32 777317022, ; 41: sk\Microsoft.Maui.Controls.resources => 0x2e54ea9e => 25
	i32 789151979, ; 42: Microsoft.Extensions.Options => 0x2f0980eb => 44
	i32 823281589, ; 43: System.Private.Uri.dll => 0x311247b5 => 99
	i32 830298997, ; 44: System.IO.Compression.Brotli => 0x317d5b75 => 88
	i32 878954865, ; 45: System.Net.Http.Json => 0x3463c971 => 93
	i32 904024072, ; 46: System.ComponentModel.Primitives.dll => 0x35e25008 => 83
	i32 926902833, ; 47: tr/Microsoft.Maui.Controls.resources.dll => 0x373f6a31 => 28
	i32 967690846, ; 48: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 62
	i32 992768348, ; 49: System.Collections.dll => 0x3b2c715c => 82
	i32 1012816738, ; 50: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 72
	i32 1028951442, ; 51: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 38
	i32 1029334545, ; 52: da/Microsoft.Maui.Controls.resources.dll => 0x3d5a6611 => 3
	i32 1035644815, ; 53: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 52
	i32 1044663988, ; 54: System.Linq.Expressions.dll => 0x3e444eb4 => 90
	i32 1048992957, ; 55: Microsoft.Extensions.Diagnostics.Abstractions.dll => 0x3e865cbd => 40
	i32 1052210849, ; 56: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 64
	i32 1082857460, ; 57: System.ComponentModel.TypeConverter => 0x408b17f4 => 84
	i32 1084122840, ; 58: Xamarin.Kotlin.StdLib => 0x409e66d8 => 77
	i32 1098259244, ; 59: System => 0x41761b2c => 111
	i32 1118262833, ; 60: ko\Microsoft.Maui.Controls.resources => 0x42a75631 => 16
	i32 1142409116, ; 61: Taller 3 => 0x4417c79c => 79
	i32 1168523401, ; 62: pt\Microsoft.Maui.Controls.resources => 0x45a64089 => 22
	i32 1178241025, ; 63: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 69
	i32 1203215381, ; 64: pl/Microsoft.Maui.Controls.resources.dll => 0x47b79c15 => 20
	i32 1234928153, ; 65: nb/Microsoft.Maui.Controls.resources.dll => 0x499b8219 => 18
	i32 1260983243, ; 66: cs\Microsoft.Maui.Controls.resources => 0x4b2913cb => 2
	i32 1293217323, ; 67: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 60
	i32 1324164729, ; 68: System.Linq => 0x4eed2679 => 91
	i32 1373134921, ; 69: zh-Hans\Microsoft.Maui.Controls.resources => 0x51d86049 => 32
	i32 1376866003, ; 70: Xamarin.AndroidX.SavedState => 0x52114ed3 => 72
	i32 1394371233, ; 71: Taller 3.dll => 0x531c6aa1 => 79
	i32 1406073936, ; 72: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 56
	i32 1430672901, ; 73: ar\Microsoft.Maui.Controls.resources => 0x55465605 => 0
	i32 1461004990, ; 74: es\Microsoft.Maui.Controls.resources => 0x57152abe => 6
	i32 1462112819, ; 75: System.IO.Compression.dll => 0x57261233 => 89
	i32 1469204771, ; 76: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 53
	i32 1470490898, ; 77: Microsoft.Extensions.Primitives => 0x57a5e912 => 45
	i32 1480492111, ; 78: System.IO.Compression.Brotli.dll => 0x583e844f => 88
	i32 1493001747, ; 79: hi/Microsoft.Maui.Controls.resources.dll => 0x58fd6613 => 10
	i32 1505131794, ; 80: Microsoft.Extensions.Http => 0x59b67d12 => 41
	i32 1514721132, ; 81: el/Microsoft.Maui.Controls.resources.dll => 0x5a48cf6c => 5
	i32 1543031311, ; 82: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 107
	i32 1551623176, ; 83: sk/Microsoft.Maui.Controls.resources.dll => 0x5c7be408 => 25
	i32 1622152042, ; 84: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 66
	i32 1624863272, ; 85: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 75
	i32 1636350590, ; 86: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 58
	i32 1639515021, ; 87: System.Net.Http.dll => 0x61b9038d => 94
	i32 1639986890, ; 88: System.Text.RegularExpressions => 0x61c036ca => 107
	i32 1657153582, ; 89: System.Runtime => 0x62c6282e => 103
	i32 1658251792, ; 90: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 76
	i32 1677501392, ; 91: System.Net.Primitives.dll => 0x63fca3d0 => 95
	i32 1679769178, ; 92: System.Security.Cryptography => 0x641f3e5a => 104
	i32 1729485958, ; 93: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 54
	i32 1736233607, ; 94: ro/Microsoft.Maui.Controls.resources.dll => 0x677cd287 => 23
	i32 1743415430, ; 95: ca\Microsoft.Maui.Controls.resources => 0x67ea6886 => 1
	i32 1766324549, ; 96: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 73
	i32 1770582343, ; 97: Microsoft.Extensions.Logging.dll => 0x6988f147 => 42
	i32 1780572499, ; 98: Mono.Android.Runtime.dll => 0x6a216153 => 114
	i32 1782862114, ; 99: ms\Microsoft.Maui.Controls.resources => 0x6a445122 => 17
	i32 1788241197, ; 100: Xamarin.AndroidX.Fragment => 0x6a96652d => 61
	i32 1793755602, ; 101: he\Microsoft.Maui.Controls.resources => 0x6aea89d2 => 9
	i32 1808609942, ; 102: Xamarin.AndroidX.Loader => 0x6bcd3296 => 66
	i32 1813058853, ; 103: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 77
	i32 1813201214, ; 104: Xamarin.Google.Android.Material => 0x6c13413e => 76
	i32 1818569960, ; 105: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 70
	i32 1828688058, ; 106: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 43
	i32 1842015223, ; 107: uk/Microsoft.Maui.Controls.resources.dll => 0x6dcaebf7 => 29
	i32 1853025655, ; 108: sv\Microsoft.Maui.Controls.resources => 0x6e72ed77 => 26
	i32 1858542181, ; 109: System.Linq.Expressions => 0x6ec71a65 => 90
	i32 1875935024, ; 110: fr\Microsoft.Maui.Controls.resources => 0x6fd07f30 => 8
	i32 1910275211, ; 111: System.Collections.NonGeneric.dll => 0x71dc7c8b => 81
	i32 1968388702, ; 112: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 35
	i32 2003115576, ; 113: el\Microsoft.Maui.Controls.resources => 0x77651e38 => 5
	i32 2019465201, ; 114: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 64
	i32 2025202353, ; 115: ar/Microsoft.Maui.Controls.resources.dll => 0x78b622b1 => 0
	i32 2045470958, ; 116: System.Private.Xml => 0x79eb68ee => 100
	i32 2055257422, ; 117: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 63
	i32 2066184531, ; 118: de\Microsoft.Maui.Controls.resources => 0x7b277953 => 4
	i32 2079903147, ; 119: System.Runtime.dll => 0x7bf8cdab => 103
	i32 2090596640, ; 120: System.Numerics.Vectors => 0x7c9bf920 => 97
	i32 2127167465, ; 121: System.Console => 0x7ec9ffe9 => 86
	i32 2159891885, ; 122: Microsoft.Maui => 0x80bd55ad => 48
	i32 2169148018, ; 123: hu\Microsoft.Maui.Controls.resources => 0x814a9272 => 12
	i32 2181898931, ; 124: Microsoft.Extensions.Options.dll => 0x820d22b3 => 44
	i32 2192057212, ; 125: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 43
	i32 2193016926, ; 126: System.ObjectModel.dll => 0x82b6c85e => 98
	i32 2201107256, ; 127: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 78
	i32 2201231467, ; 128: System.Net.Http => 0x8334206b => 94
	i32 2207618523, ; 129: it\Microsoft.Maui.Controls.resources => 0x839595db => 14
	i32 2266799131, ; 130: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 36
	i32 2270573516, ; 131: fr/Microsoft.Maui.Controls.resources.dll => 0x875633cc => 8
	i32 2279755925, ; 132: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 71
	i32 2303942373, ; 133: nb\Microsoft.Maui.Controls.resources => 0x89535ee5 => 18
	i32 2305521784, ; 134: System.Private.CoreLib.dll => 0x896b7878 => 112
	i32 2353062107, ; 135: System.Net.Primitives => 0x8c40e0db => 95
	i32 2368005991, ; 136: System.Xml.ReaderWriter.dll => 0x8d24e767 => 110
	i32 2371007202, ; 137: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 35
	i32 2395872292, ; 138: id\Microsoft.Maui.Controls.resources => 0x8ece1c24 => 13
	i32 2427813419, ; 139: hi\Microsoft.Maui.Controls.resources => 0x90b57e2b => 10
	i32 2435356389, ; 140: System.Console.dll => 0x912896e5 => 86
	i32 2475788418, ; 141: Java.Interop.dll => 0x93918882 => 113
	i32 2480646305, ; 142: Microsoft.Maui.Controls => 0x93dba8a1 => 46
	i32 2550873716, ; 143: hr\Microsoft.Maui.Controls.resources => 0x980b3e74 => 11
	i32 2570120770, ; 144: System.Text.Encodings.Web => 0x9930ee42 => 105
	i32 2593496499, ; 145: pl\Microsoft.Maui.Controls.resources => 0x9a959db3 => 20
	i32 2605712449, ; 146: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 78
	i32 2617129537, ; 147: System.Private.Xml.dll => 0x9bfe3a41 => 100
	i32 2620871830, ; 148: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 58
	i32 2626831493, ; 149: ja\Microsoft.Maui.Controls.resources => 0x9c924485 => 15
	i32 2663698177, ; 150: System.Runtime.Loader => 0x9ec4cf01 => 102
	i32 2732626843, ; 151: Xamarin.AndroidX.Activity => 0xa2e0939b => 51
	i32 2737747696, ; 152: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 53
	i32 2752995522, ; 153: pt-BR\Microsoft.Maui.Controls.resources => 0xa41760c2 => 21
	i32 2758225723, ; 154: Microsoft.Maui.Controls.Xaml => 0xa4672f3b => 47
	i32 2764765095, ; 155: Microsoft.Maui.dll => 0xa4caf7a7 => 48
	i32 2778768386, ; 156: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 74
	i32 2785988530, ; 157: th\Microsoft.Maui.Controls.resources => 0xa60ecfb2 => 27
	i32 2801831435, ; 158: Microsoft.Maui.Graphics => 0xa7008e0b => 50
	i32 2806116107, ; 159: es/Microsoft.Maui.Controls.resources.dll => 0xa741ef0b => 6
	i32 2810250172, ; 160: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 56
	i32 2831556043, ; 161: nl/Microsoft.Maui.Controls.resources.dll => 0xa8c61dcb => 19
	i32 2853208004, ; 162: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 74
	i32 2861189240, ; 163: Microsoft.Maui.Essentials => 0xaa8a4878 => 49
	i32 2909740682, ; 164: System.Private.CoreLib => 0xad6f1e8a => 112
	i32 2916838712, ; 165: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 75
	i32 2919462931, ; 166: System.Numerics.Vectors.dll => 0xae037813 => 97
	i32 2959614098, ; 167: System.ComponentModel.dll => 0xb0682092 => 85
	i32 2978675010, ; 168: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 60
	i32 3020703001, ; 169: Microsoft.Extensions.Diagnostics => 0xb40c4519 => 39
	i32 3038032645, ; 170: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 34
	i32 3057625584, ; 171: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 67
	i32 3059408633, ; 172: Mono.Android.Runtime => 0xb65adef9 => 114
	i32 3059793426, ; 173: System.ComponentModel.Primitives => 0xb660be12 => 83
	i32 3077302341, ; 174: hu/Microsoft.Maui.Controls.resources.dll => 0xb76be845 => 12
	i32 3178803400, ; 175: Xamarin.AndroidX.Navigation.Fragment.dll => 0xbd78b0c8 => 68
	i32 3220365878, ; 176: System.Threading => 0xbff2e236 => 109
	i32 3258312781, ; 177: Xamarin.AndroidX.CardView => 0xc235e84d => 54
	i32 3305363605, ; 178: fi\Microsoft.Maui.Controls.resources => 0xc503d895 => 7
	i32 3316684772, ; 179: System.Net.Requests.dll => 0xc5b097e4 => 96
	i32 3317135071, ; 180: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 59
	i32 3346324047, ; 181: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 69
	i32 3357674450, ; 182: ru\Microsoft.Maui.Controls.resources => 0xc8220bd2 => 24
	i32 3358260929, ; 183: System.Text.Json => 0xc82afec1 => 106
	i32 3362522851, ; 184: Xamarin.AndroidX.Core => 0xc86c06e3 => 57
	i32 3366347497, ; 185: Java.Interop => 0xc8a662e9 => 113
	i32 3374999561, ; 186: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 71
	i32 3381016424, ; 187: da\Microsoft.Maui.Controls.resources => 0xc9863768 => 3
	i32 3428513518, ; 188: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 37
	i32 3463511458, ; 189: hr/Microsoft.Maui.Controls.resources.dll => 0xce70fda2 => 11
	i32 3471940407, ; 190: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 84
	i32 3476120550, ; 191: Mono.Android => 0xcf3163e6 => 115
	i32 3479583265, ; 192: ru/Microsoft.Maui.Controls.resources.dll => 0xcf663a21 => 24
	i32 3484440000, ; 193: ro\Microsoft.Maui.Controls.resources => 0xcfb055c0 => 23
	i32 3485117614, ; 194: System.Text.Json.dll => 0xcfbaacae => 106
	i32 3580758918, ; 195: zh-HK\Microsoft.Maui.Controls.resources => 0xd56e0b86 => 31
	i32 3608519521, ; 196: System.Linq.dll => 0xd715a361 => 91
	i32 3641597786, ; 197: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 63
	i32 3643446276, ; 198: tr\Microsoft.Maui.Controls.resources => 0xd92a9404 => 28
	i32 3643854240, ; 199: Xamarin.AndroidX.Navigation.Fragment => 0xd930cda0 => 68
	i32 3657292374, ; 200: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 36
	i32 3672681054, ; 201: Mono.Android.dll => 0xdae8aa5e => 115
	i32 3697841164, ; 202: zh-Hant/Microsoft.Maui.Controls.resources.dll => 0xdc68940c => 33
	i32 3724971120, ; 203: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 67
	i32 3737834244, ; 204: System.Net.Http.Json.dll => 0xdecad304 => 93
	i32 3748608112, ; 205: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 87
	i32 3786282454, ; 206: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 55
	i32 3792276235, ; 207: System.Collections.NonGeneric => 0xe2098b0b => 81
	i32 3823082795, ; 208: System.Security.Cryptography.dll => 0xe3df9d2b => 104
	i32 3841636137, ; 209: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 38
	i32 3849253459, ; 210: System.Runtime.InteropServices.dll => 0xe56ef253 => 101
	i32 3889960447, ; 211: zh-Hans/Microsoft.Maui.Controls.resources.dll => 0xe7dc15ff => 32
	i32 3896106733, ; 212: System.Collections.Concurrent.dll => 0xe839deed => 80
	i32 3896760992, ; 213: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 57
	i32 3928044579, ; 214: System.Xml.ReaderWriter => 0xea213423 => 110
	i32 3931092270, ; 215: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 70
	i32 3955647286, ; 216: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 52
	i32 3980434154, ; 217: th/Microsoft.Maui.Controls.resources.dll => 0xed409aea => 27
	i32 3987592930, ; 218: he/Microsoft.Maui.Controls.resources.dll => 0xedadd6e2 => 9
	i32 4025784931, ; 219: System.Memory => 0xeff49a63 => 92
	i32 4046471985, ; 220: Microsoft.Maui.Controls.Xaml.dll => 0xf1304331 => 47
	i32 4073602200, ; 221: System.Threading.dll => 0xf2ce3c98 => 109
	i32 4094352644, ; 222: Microsoft.Maui.Essentials.dll => 0xf40add04 => 49
	i32 4100113165, ; 223: System.Private.Uri => 0xf462c30d => 99
	i32 4102112229, ; 224: pt/Microsoft.Maui.Controls.resources.dll => 0xf48143e5 => 22
	i32 4125707920, ; 225: ms/Microsoft.Maui.Controls.resources.dll => 0xf5e94e90 => 17
	i32 4126470640, ; 226: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 37
	i32 4150914736, ; 227: uk\Microsoft.Maui.Controls.resources => 0xf769eeb0 => 29
	i32 4182413190, ; 228: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 65
	i32 4213026141, ; 229: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 87
	i32 4271975918, ; 230: Microsoft.Maui.Controls.dll => 0xfea12dee => 46
	i32 4292120959 ; 231: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 65
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [232 x i32] [
	i32 108, ; 0
	i32 33, ; 1
	i32 50, ; 2
	i32 40, ; 3
	i32 101, ; 4
	i32 55, ; 5
	i32 73, ; 6
	i32 30, ; 7
	i32 31, ; 8
	i32 85, ; 9
	i32 39, ; 10
	i32 2, ; 11
	i32 41, ; 12
	i32 30, ; 13
	i32 51, ; 14
	i32 15, ; 15
	i32 62, ; 16
	i32 14, ; 17
	i32 108, ; 18
	i32 92, ; 19
	i32 34, ; 20
	i32 26, ; 21
	i32 82, ; 22
	i32 61, ; 23
	i32 111, ; 24
	i32 98, ; 25
	i32 13, ; 26
	i32 7, ; 27
	i32 45, ; 28
	i32 42, ; 29
	i32 21, ; 30
	i32 59, ; 31
	i32 19, ; 32
	i32 105, ; 33
	i32 80, ; 34
	i32 1, ; 35
	i32 16, ; 36
	i32 4, ; 37
	i32 102, ; 38
	i32 96, ; 39
	i32 89, ; 40
	i32 25, ; 41
	i32 44, ; 42
	i32 99, ; 43
	i32 88, ; 44
	i32 93, ; 45
	i32 83, ; 46
	i32 28, ; 47
	i32 62, ; 48
	i32 82, ; 49
	i32 72, ; 50
	i32 38, ; 51
	i32 3, ; 52
	i32 52, ; 53
	i32 90, ; 54
	i32 40, ; 55
	i32 64, ; 56
	i32 84, ; 57
	i32 77, ; 58
	i32 111, ; 59
	i32 16, ; 60
	i32 79, ; 61
	i32 22, ; 62
	i32 69, ; 63
	i32 20, ; 64
	i32 18, ; 65
	i32 2, ; 66
	i32 60, ; 67
	i32 91, ; 68
	i32 32, ; 69
	i32 72, ; 70
	i32 79, ; 71
	i32 56, ; 72
	i32 0, ; 73
	i32 6, ; 74
	i32 89, ; 75
	i32 53, ; 76
	i32 45, ; 77
	i32 88, ; 78
	i32 10, ; 79
	i32 41, ; 80
	i32 5, ; 81
	i32 107, ; 82
	i32 25, ; 83
	i32 66, ; 84
	i32 75, ; 85
	i32 58, ; 86
	i32 94, ; 87
	i32 107, ; 88
	i32 103, ; 89
	i32 76, ; 90
	i32 95, ; 91
	i32 104, ; 92
	i32 54, ; 93
	i32 23, ; 94
	i32 1, ; 95
	i32 73, ; 96
	i32 42, ; 97
	i32 114, ; 98
	i32 17, ; 99
	i32 61, ; 100
	i32 9, ; 101
	i32 66, ; 102
	i32 77, ; 103
	i32 76, ; 104
	i32 70, ; 105
	i32 43, ; 106
	i32 29, ; 107
	i32 26, ; 108
	i32 90, ; 109
	i32 8, ; 110
	i32 81, ; 111
	i32 35, ; 112
	i32 5, ; 113
	i32 64, ; 114
	i32 0, ; 115
	i32 100, ; 116
	i32 63, ; 117
	i32 4, ; 118
	i32 103, ; 119
	i32 97, ; 120
	i32 86, ; 121
	i32 48, ; 122
	i32 12, ; 123
	i32 44, ; 124
	i32 43, ; 125
	i32 98, ; 126
	i32 78, ; 127
	i32 94, ; 128
	i32 14, ; 129
	i32 36, ; 130
	i32 8, ; 131
	i32 71, ; 132
	i32 18, ; 133
	i32 112, ; 134
	i32 95, ; 135
	i32 110, ; 136
	i32 35, ; 137
	i32 13, ; 138
	i32 10, ; 139
	i32 86, ; 140
	i32 113, ; 141
	i32 46, ; 142
	i32 11, ; 143
	i32 105, ; 144
	i32 20, ; 145
	i32 78, ; 146
	i32 100, ; 147
	i32 58, ; 148
	i32 15, ; 149
	i32 102, ; 150
	i32 51, ; 151
	i32 53, ; 152
	i32 21, ; 153
	i32 47, ; 154
	i32 48, ; 155
	i32 74, ; 156
	i32 27, ; 157
	i32 50, ; 158
	i32 6, ; 159
	i32 56, ; 160
	i32 19, ; 161
	i32 74, ; 162
	i32 49, ; 163
	i32 112, ; 164
	i32 75, ; 165
	i32 97, ; 166
	i32 85, ; 167
	i32 60, ; 168
	i32 39, ; 169
	i32 34, ; 170
	i32 67, ; 171
	i32 114, ; 172
	i32 83, ; 173
	i32 12, ; 174
	i32 68, ; 175
	i32 109, ; 176
	i32 54, ; 177
	i32 7, ; 178
	i32 96, ; 179
	i32 59, ; 180
	i32 69, ; 181
	i32 24, ; 182
	i32 106, ; 183
	i32 57, ; 184
	i32 113, ; 185
	i32 71, ; 186
	i32 3, ; 187
	i32 37, ; 188
	i32 11, ; 189
	i32 84, ; 190
	i32 115, ; 191
	i32 24, ; 192
	i32 23, ; 193
	i32 106, ; 194
	i32 31, ; 195
	i32 91, ; 196
	i32 63, ; 197
	i32 28, ; 198
	i32 68, ; 199
	i32 36, ; 200
	i32 115, ; 201
	i32 33, ; 202
	i32 67, ; 203
	i32 93, ; 204
	i32 87, ; 205
	i32 55, ; 206
	i32 81, ; 207
	i32 104, ; 208
	i32 38, ; 209
	i32 101, ; 210
	i32 32, ; 211
	i32 80, ; 212
	i32 57, ; 213
	i32 110, ; 214
	i32 70, ; 215
	i32 52, ; 216
	i32 27, ; 217
	i32 9, ; 218
	i32 92, ; 219
	i32 47, ; 220
	i32 109, ; 221
	i32 49, ; 222
	i32 99, ; 223
	i32 22, ; 224
	i32 17, ; 225
	i32 37, ; 226
	i32 29, ; 227
	i32 65, ; 228
	i32 87, ; 229
	i32 46, ; 230
	i32 65 ; 231
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 4

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 4

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 4

; Functions

; Function attributes: "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 4, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 1

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-thumb-mode,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-thumb-mode,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }

; Metadata
!llvm.module.flags = !{!0, !1, !7}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.4xx @ 82d8938cf80f6d5fa6c28529ddfbdb753d805ab4"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"min_enum_size", i32 4}
